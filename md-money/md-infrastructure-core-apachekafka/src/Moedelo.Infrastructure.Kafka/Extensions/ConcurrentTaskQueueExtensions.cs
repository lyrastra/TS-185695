using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.Kafka.Extensions;

internal static class ConcurrentTaskQueueExtensions
{
    internal static async ValueTask SafeClearAndWaitAsync(this ConcurrentQueue<Task> queue, TimeSpan maxTimeout)
    {
        try
        {
            await Task.WhenAny(
                Task.WhenAll(queue.DequeueAll()),
                Task.Delay(maxTimeout));
        }
        catch
        {
            // nothing to do
        }
    }

    private static IEnumerable<T> DequeueAll<T>(this ConcurrentQueue<T> queue)
    {
        while (queue.TryDequeue(out var item))
            yield return item;
    }
}
