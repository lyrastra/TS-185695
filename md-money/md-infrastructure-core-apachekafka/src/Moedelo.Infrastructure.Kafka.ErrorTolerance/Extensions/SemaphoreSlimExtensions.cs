namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Extensions;

internal static class SemaphoreSlimExtensions
{
    private class SemaphoreReleaseOnDispose : IDisposable
    {
        private readonly SemaphoreSlim semaphore;

        public SemaphoreReleaseOnDispose(SemaphoreSlim semaphore)
        {
            this.semaphore = semaphore;
        }

        public void Dispose()
        {
            semaphore.Release();
        }
    }

    internal static async ValueTask<IDisposable> CaptureAsync(
        this SemaphoreSlim semaphore,
        CancellationToken cancellationToken = default)
    {
        await semaphore.WaitAsync(cancellationToken);

        return new SemaphoreReleaseOnDispose(semaphore);
    }

    internal static IDisposable Capture(
        this SemaphoreSlim semaphore,
        CancellationToken cancellationToken = default)
    {
        semaphore.Wait(cancellationToken);

        return new SemaphoreReleaseOnDispose(semaphore);
    }
}
