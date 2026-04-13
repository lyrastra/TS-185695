using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Warmup.Extensions;

namespace Moedelo.Infrastructure.DependencyInjection.Warmup;

internal sealed class WarmupHostedService : BackgroundService
{
    private readonly IServiceCollection services;
    private readonly IServiceProvider provider;
    private readonly ILogger logger;
    private readonly ApplicationWarmupOptions options;

    public WarmupHostedService(
        IServiceCollection services,
        IServiceProvider provider,
        ILogger<WarmupHostedService> logger,
        ApplicationWarmupOptions options)
    {
        this.services = services;
        this.provider = provider;
        this.logger = logger;
        this.options = options;
    }

    [MethodImpl(MethodImplOptions.NoOptimization)]
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        using var scope = provider.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var stopWatch = Stopwatch.StartNew();
        var serviceTypes = EnumerateServicesTypes(services).ToArray();
        var exceptions = new ConcurrentQueue<Exception>();
            
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
        logger.LogWarmUpStarted(serviceTypes.Length, parallelOptions.MaxDegreeOfParallelism);

        Parallel.ForEach(serviceTypes, parallelOptions, (serviceType, loopState) =>
        {
            if (loopState.IsStopped || stoppingToken.IsCancellationRequested)
            {
                return;
            }

            try
            {
                serviceProvider.GetService(serviceType);
            }
            catch (Exception exception)
            {
                logger.LogTypeInstantiationFailed(exception, serviceType);
                exceptions.Enqueue(exception);

                if (options.StopOnFirstFailure)
                {
                    loopState.Stop();
                }
            }
        });

        stopWatch.Stop();

        if (!exceptions.IsEmpty)
        {
            var aggException = new AggregateException(
                $"Проверка создаваемости сервисов завершилась неудачно (затраченное время: {stopWatch.Elapsed:g})",
                exceptions);
            logger.LogWarmUpFailed(aggException);

            throw aggException;
        }

        logger.LogWarmUpComplete(serviceTypes.Length, stopWatch.Elapsed);
    }

    private static IEnumerable<Type> EnumerateServicesTypes(IServiceCollection services)
    {
        return services
            .Where(descriptor => descriptor.ImplementationType != typeof(WarmupHostedService))
            .Where(descriptor => descriptor.ServiceType.ContainsGenericParameters == false)
            .Where(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton)
            .Select(descriptor => descriptor.ServiceType)
            .Distinct();
    }
}