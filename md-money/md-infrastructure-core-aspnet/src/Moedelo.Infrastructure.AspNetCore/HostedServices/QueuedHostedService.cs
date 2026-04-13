using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.AspNetCore.Abstractions.BackgroundTasks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.AspNetCore.HostedServices
{
    /// <summary>
    /// Сервис для фонового выполнения некритичных для бизнес-логики задач.
    /// Добавление задач в очередь происходит через отдельный сервис <see cref="IBackgroundTaskQueue"/>.
    /// Выбирает и исполняет задачи по одной друг за другом.
    /// Не гарантирует выполнение всех задач (например, при остановке приложения).
    /// Подойдет, если нужно выполнить задачу в фоне, не ожидая завершения.
    /// </summary>
    public class QueuedHostedService : BackgroundService
    {
        private static readonly TimeSpan WaitInterval = TimeSpan.FromSeconds(5);

        private readonly ILogger logger;
        private readonly IBackgroundTaskQueue taskQueue;

        public QueuedHostedService(
            ILogger<QueuedHostedService> logger,
            IBackgroundTaskQueue taskQueue)
        {
            this.logger = logger;
            this.taskQueue = taskQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            // надо как можно раньше вернуть управление в вызывающий поток, чтобы не блокировать на старте приложения
            // (ExecuteAsync вызывается из BackgroundService.StartAsync)
            await Task.Yield();

            logger.LogInformation("{Name} стартует", GetType().Name);

            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await taskQueue.DequeueAsync(cancellationToken).ConfigureAwait(false);
                if (workItem == null)
                {
                    workItem = ct => Task.Delay(WaitInterval, ct);
                }

                try
                {
                    await workItem(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Error occurred executing {nameof(workItem)}.");
                }
            }

            logger.LogInformation("{ServiceName} остановлен", GetType().Name);
        }
    }
}
