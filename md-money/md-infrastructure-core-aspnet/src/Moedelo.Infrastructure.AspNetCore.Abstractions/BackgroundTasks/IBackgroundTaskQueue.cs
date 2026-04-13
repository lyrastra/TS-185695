using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.AspNetCore.Abstractions.BackgroundTasks
{
    /// <summary>
    /// Реализация очереди асинхронных задач
    /// </summary>
    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem);

        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }
}
