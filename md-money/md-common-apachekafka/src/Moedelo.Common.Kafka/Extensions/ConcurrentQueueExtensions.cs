using System;
using System.Collections.Concurrent;

namespace Moedelo.Common.Kafka.Extensions;

internal static class ConcurrentQueueExtensions
{
    internal static void SafeClear<TDisposable>(this ConcurrentQueue<TDisposable> queue) where TDisposable : IDisposable
    {
        while(queue.TryDequeue(out var item))
        {
            try
            {
                item.Dispose();
            }
            catch
            {
                /* ошибки игнорируем */
            }
        }
    }
}
