using Microsoft.Extensions.Logging;
using Moedelo.Common.Consul.Abstractions;
using Moedelo.Common.Consul.ServiceDiscovery.Abstractions;
using Moedelo.Common.Consul.ServiceDiscovery.Internals.Extensions;
using Moedelo.Common.Consul.ServiceDiscovery.Internals.Models;
using Moedelo.Infrastructure.Consul.Abstraction.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Consul.ServiceDiscovery.Internals;

[InjectAsSingleton(typeof(IMoedeloConsulServiceDiscovery))]
internal sealed class MoedeloConsulServiceDiscovery : IMoedeloConsulServiceDiscovery, IDisposable
{
    private const string DtFormat = "yyyy-MM-dd HH:mm:sszzz";

    private readonly IMoedeloServiceDiscoverySettings serviceDiscoverySettings;
    private readonly IMoedeloConsulAgentApiClient consulAgentApi;
    private readonly ILogger logger;

    private readonly CancellationTokenSource cancellationDueToDispose = new();
    private readonly TaskCompletionSource<ServiceRegistrationInfo> registrationDone = new ();
    private LogLevel LogLevel => serviceDiscoverySettings.LogLevel;

    public MoedeloConsulServiceDiscovery(
        IMoedeloServiceDiscoverySettings serviceDiscoverySettings,
        ILogger<MoedeloConsulServiceDiscovery> logger,
        IMoedeloConsulAgentApiClient consulClient)
    {
        this.serviceDiscoverySettings = serviceDiscoverySettings;
        this.logger = logger;
        this.consulAgentApi = consulClient;
    }

    public void Dispose()
    {
        if (!cancellationDueToDispose.IsCancellationRequested)
        {
            cancellationDueToDispose.Cancel();
        }
    }

    public string ServiceId => serviceDiscoverySettings.ServiceId;

    public async Task ExecuteAsync(Uri listenerAddress, CancellationToken cancellationToken)
    {
        using var cancellation = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationToken,
            cancellationDueToDispose.Token);

        var regInfo = await RegisterServiceAsync(listenerAddress, cancellation.Token).ConfigureAwait(false);

        try
        {
            await StartHealthPassSendingLoopAsync(
                    regInfo,
                    cancellation.Token)
                .ConfigureAwait(false);
        }
        finally
        {
            using var stopTimeout = new CancellationTokenSource(TimeSpan.FromSeconds(6));
            await UnregisterServiceAsync(regInfo, stopTimeout.Token).ConfigureAwait(false);
        }
    }

    public async Task WaitForRegistrationCompleteAsync(CancellationToken cancellationToken)
    {
        var timeout = serviceDiscoverySettings.InitialRegistrationWaitTimeout;

        using var cancelByTimeout = new CancellationTokenSource(timeout);
        using var cancellation = CancellationTokenSource
            .CreateLinkedTokenSource(cancelByTimeout.Token, cancellationToken, cancellationDueToDispose.Token);
        await using var registration = cancellation.Token.Register(() => registrationDone.TrySetCanceled());

        try
        {
            await registrationDone.Task.ConfigureAwait(false);
        }
        catch (OperationCanceledException exception) when (cancelByTimeout.IsCancellationRequested)
        {
            throw new AggregateException($"Не удалось дождаться завершения регистрации приложения в Service Discovery за {timeout:g}. Проверьте, что в Startup.cs приложения добавлен вызов services.AddMoedeloServiceDiscovery()", exception);
        }
    }

    private async Task<ServiceRegistrationInfo> RegisterServiceAsync(
        Uri listenerAddress,
        CancellationToken cancellation)
    {
        AgentServiceRegistration? serviceRegistration = default;

        try
        {
            serviceRegistration = serviceDiscoverySettings.CreateAgentServiceRegistration(listenerAddress, DtFormat);
            var regInfo = new ServiceRegistrationInfo(serviceRegistration);

            await RegisterServiceAsync(regInfo, isRestoringAfterAbandonment: false, cancellation);

            registrationDone.TrySetResult(regInfo);

            return regInfo;
        }
        catch (Exception exception)
        {
            logger.LogRegistrationError(exception, serviceRegistration?.ID);
            throw;
        }
        finally
        {
            if (!registrationDone.Task.IsCompleted)
            {
                registrationDone.TrySetResult(new ServiceRegistrationInfo(serviceRegistration));
            }
        }
    }

    private async Task RegisterServiceAsync(
        ServiceRegistrationInfo regInfo,
        bool isRestoringAfterAbandonment,
        CancellationToken cancellation)
    {
        var serviceRegistration = regInfo.ServiceRegistration ??
                                  throw new ArgumentNullException(nameof(regInfo.ServiceRegistration),
                                      "Не заполнены регистрационные данные");
        
        // зачистка регистраций с такими же идентификаторами (на всякий случай)
        await consulAgentApi.DeregisterServiceAsync(serviceRegistration.ID, cancellation).ConfigureAwait(false);

        // регистрация службы и способа проверки её здоровья
        await consulAgentApi.RegisterServiceAsync(serviceRegistration, cancellation).ConfigureAwait(false);

        if (!isRestoringAfterAbandonment)
        {
            logger.LogFirstRegistration(LogLevel, serviceRegistration.ID);
        }
        else
        {
            logger.LogRepeatedRegistration(LogLevel, serviceRegistration.ID);
        }
    }

    private async Task UnregisterServiceAsync(
        ServiceRegistrationInfo regInfo,
        CancellationToken cancellationToken)
    {
        if (!registrationDone.Task.IsCompleted)
        {
            logger.LogError("Невозможно снять службу с регистрации в Consul, поскольку регистрация не закончена");
            return;
        }

        try
        {
            if (regInfo.ServiceRegistrationId != null)
            {
                logger.LogSendingRegistration(regInfo.ServiceRegistrationId);
                    
                await consulAgentApi
                    .DeregisterServiceAsync(regInfo.ServiceRegistrationId, cancellationToken)
                    .ConfigureAwait(false);
            }

            logger.LogDeregistrationDone(LogLevel, regInfo.ServiceRegistrationId);
        }
        catch (Exception exception)
        {
            logger.LogDeregistrationError(exception, regInfo.ServiceRegistrationId);
        }
    }

    private async Task StartHealthPassSendingLoopAsync(
        ServiceRegistrationInfo regInfo,
        CancellationToken cancellation)
    {
        var successSentCount = 0;

        var serviceRegistrationId = regInfo.ServiceRegistrationId!;
        var checkAgentId = regInfo.CheckRegistrationId!;

        try
        {
            while (!cancellation.IsCancellationRequested)
            {
                try
                {
                    var isExist = await consulAgentApi
                        .IsServiceRegisteredAsync(serviceRegistrationId, cancellation)
                        .ConfigureAwait(false);

                    if (!isExist)
                    {
                        logger.LogRegistrationIsGoneError(serviceRegistrationId);

                        await RegisterServiceAsync(regInfo, isRestoringAfterAbandonment: true, cancellation);
                    }
                        
                    await consulAgentApi
                        .SendServiceCheckTtlAsync(checkAgentId, $"checked at {DateTime.Now.ToString(DtFormat)}", cancellation)
                        .ConfigureAwait(false);

                    successSentCount += 1;

                    if (serviceDiscoverySettings.LogEveryNthSuccessfulPass > 0 && successSentCount >= serviceDiscoverySettings.LogEveryNthSuccessfulPass)
                    {
                        logger.LogServiceHealthStatusGood(serviceRegistrationId, successSentCount);

                        successSentCount = 0;
                    }
                }
                catch (Exception exception) when(!cancellation.IsCancellationRequested)
                {
                    logger.LogHealthCheckSendingError(exception, serviceRegistrationId);
                }

                await Task.Delay(serviceDiscoverySettings.HealthPassTimeout, cancellation).ConfigureAwait(false);
            }
        }
        catch (Exception exception) when (exception is not TaskCanceledException)
        {
            logger.LogHealthCheckLoopInterruptedError(exception, serviceRegistrationId);
        }
        finally
        {
            logger.LogHealthCheckLoopEnded(serviceRegistrationId);
        }
    }
}