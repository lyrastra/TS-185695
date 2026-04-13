using Moedelo.Infrastructure.AspNetCore.Abstractions.BackgroundTasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.AspNetCore.BackgroundTasks
{
    [InjectAsSingleton(typeof(IBackgroundTaskQueue))]
    internal class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly ConcurrentQueue<Func<CancellationToken, Task>> workItems = new ConcurrentQueue<Func<CancellationToken, Task>>();
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(0);

        public void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }
            
            workItems.Enqueue(workItem);
            semaphore.Release();
        }

        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            workItems.TryDequeue(out var workItem);
            
            return workItem;
        }
    }
}