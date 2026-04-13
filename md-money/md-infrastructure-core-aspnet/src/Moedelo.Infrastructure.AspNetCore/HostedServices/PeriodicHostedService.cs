using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.AspNetCore.HostedServices;

/// <summary>
/// Наивная реализация периодического запуска одной задачи:
/// в цикле выполняется задача, потом ожидается заданное время (5 секунд по умолчанию).
/// Величина задержки зависит от времени выполнения задачи.
/// Подойдет для большинства случаев, когда нужно время от времени вызывать одну и ту же задачу.
/// Обеспечивает логирование и подавление распространения ошибок (try-catch-logging)
/// ВНИМАНИЕ: в продуктовом коде используй MoedeloPeriodicHostedService из md-common-aspnet
/// </summary>
public abstract class PeriodicHostedService : BackgroundService
{
    private bool isStarted;
    protected readonly ILogger logger;

    protected PeriodicHostedService(ILogger logger)
    {
        this.logger = logger;
    }

    protected virtual TimeSpan Interval => TimeSpan.FromSeconds(5);

    public sealed override Task StartAsync(CancellationToken cancellationToken)
    {
        this.OnBeforeStart();
        isStarted = true;
        return base.StartAsync(cancellationToken);
    }

    public sealed override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (isStarted)
        {
            await OnBeforeStopAsync(cancellationToken);
        }

        await base.StopAsync(cancellationToken);
    }

    protected virtual Task OnBeforeStopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Этот метод вызывается перед StartAsync.
    /// В нём можно разместить код, который должен выполниться до запуска задачи, например, валидацию конфигурации
    /// </summary>
    protected virtual void OnBeforeStart()
    {
    }

    protected abstract Task ExecuteTaskAsync(CancellationToken cancellationToken);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        // надо как можно раньше вернуть управление в вызывающий поток, чтобы не блокировать на старте приложения
        // (ExecuteAsync вызывается из BackgroundService.StartAsync)
        await Task.Yield();

        logger.LogInformation("{ServiceName} запущен", GetType().Name);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await ExecuteTaskAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Во время выполнения задания по расписанию произошла ошибка");
            }

            try
            {
                await Task.Delay(Interval, cancellationToken).ConfigureAwait(false);
            }
            catch(TaskCanceledException)
            {
                break;
            }
        }

        logger.LogInformation("{ServiceName} остановлен", GetType().Name);
    }
}