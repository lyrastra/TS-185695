using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.Consul.Internals;

internal sealed class WatchingTask : IAsyncDisposable, IDisposable
{
    private readonly CancellationTokenSource cancellationSource;
    private readonly Task task;
    private bool isDisposed;

    private static TimeSpan TaskCompleteTimeout => TimeSpan.FromSeconds(1);

    private WatchingTask(string name, Task task, CancellationTokenSource cancellationSource)
    {
        Name = name;
        this.task = task;
        this.cancellationSource = cancellationSource;
        this.isDisposed = false;
    }

    public string Name { get; }

    public static WatchingTask StartLongRunningTask(
        string name,
        Func<CancellationToken, Task> action,
        CancellationTokenSource cancellationTokenSource)
    {
        var task = Task.Factory.StartNew(
                () => action.Invoke(cancellationTokenSource.Token),
                TaskCreationOptions.LongRunning)
            .Unwrap();

        return new WatchingTask(name, task, cancellationTokenSource);
    }

    public void Dispose()
    {
        if (isDisposed)
        {
            return;
        }

        if (!cancellationSource.IsCancellationRequested)
        {
            cancellationSource.Cancel();
        }

        if (!task.IsCanceled && !task.IsCompleted && !task.IsFaulted)
        {
            try { task.Wait(TaskCompleteTimeout); } catch {/*игнорируем ошибки*/}
        }

        isDisposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (isDisposed)
        {
            return;
        }

        if (!cancellationSource.IsCancellationRequested)
        {
            cancellationSource.Cancel();
        }

        if (!task.IsCanceled && !task.IsCompleted && !task.IsFaulted)
        {
            try
            {
                await Task.WhenAny(
                        task,
                        Task.Delay(TaskCompleteTimeout))
                    .ConfigureAwait(false);
            }catch{/*nothing*/}
        }

        cancellationSource.Dispose();
        isDisposed = true;
    }
}
