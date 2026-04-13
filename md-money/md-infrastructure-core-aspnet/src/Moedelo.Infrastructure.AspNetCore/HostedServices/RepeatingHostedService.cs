using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Moedelo.Infrastructure.AspNetCore.HostedServices;

/// <summary>
/// Наивная реализация периодического запуска одной задачи через заданные промежутки времени:
/// в цикле:
/// - выполняется задача,
/// - ожидается заданное время до окончания заданного периода(5 секунд по умолчанию).
/// - если задача выполнялась дольше заданного периода, то поведение регулируется настройкой <see cref="IntervalOverlapHandlingRule"/>
/// Величина задержки зависит от времени выполнения задачи.
/// Подойдет для большинства случаев, когда нужно время от времени вызывать одну и ту же задачу.
/// Обеспечивает логирование и подавление распространения ошибок (try-catch-logging)
/// ВНИМАНИЕ: в продуктовом коде используй MoedeloRepeatingHostedService из md-common-aspnet
/// </summary>
public abstract class RepeatingHostedService : BackgroundService
{
    protected readonly ILogger logger;

    protected RepeatingHostedService(ILogger logger)
    {
        this.logger = logger;
    }

    protected virtual TimeSpan Interval => TimeSpan.FromSeconds(5);
    protected virtual IntervalOverlapHandlingRule IntervalOverlapHandlingRule => IntervalOverlapHandlingRule.WaitForNextInterval;

    protected abstract Task ExecuteTaskAsync(CancellationToken stoppingToken);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        // надо как можно раньше вернуть управление в вызывающий поток, чтобы не блокировать на старте приложения
        // (ExecuteAsync вызывается из BackgroundService.StartAsync)
        await Task.Yield();

        logger.LogInformation("{Name} стартует", GetType().Name);

        while (!cancellationToken.IsCancellationRequested)
        {
            var startTime = DateTime.Now;
            var interval = Interval;
            var nextStartTime = startTime.Add(Interval);

            try
            {
                await ExecuteTaskAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Во время выполнения задания по расписанию произошла ошибка");
            }

            var endTime = DateTime.Now;

            if (nextStartTime < endTime)
            {
                // закончили выполнение слишком поздно
                if (IntervalOverlapHandlingRule == IntervalOverlapHandlingRule.RunImmediately)
                {
                    // в этом случае просто сразу запускаем следующий шаг
                    continue;
                }

                // иначе надо продвинуться до начала следующего интервала
                while (nextStartTime < endTime)
                {
                    nextStartTime = nextStartTime.Add(interval);
                }
            }

            Debug.Assert(endTime <= nextStartTime, "Какие-то проблемы в расчётах времени");

            try
            {
                var delayTailSpan = nextStartTime - endTime; 

                await Task.Delay(delayTailSpan, cancellationToken).ConfigureAwait(false);
            }
            catch(TaskCanceledException)
            {
                break;
            }
        }

        logger.LogInformation("{ServiceName} остановлен", GetType().Name);
    }
}
