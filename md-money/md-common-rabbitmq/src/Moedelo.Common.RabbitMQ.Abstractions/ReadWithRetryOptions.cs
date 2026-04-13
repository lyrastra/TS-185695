using System;

namespace Moedelo.Common.RabbitMQ.Abstractions
{
    public sealed class ReadWithRetryOptions
    {
        public ReadWithRetryOptions(
            uint maxRetryCount = 50, 
            Func<uint, TimeSpan> delayFunc = null,
            ushort prefetchCount = 50)
        {
            MaxRetryCount = maxRetryCount;
            DelayFunc = delayFunc ?? (_ => TimeSpan.FromMinutes(30));
            PrefetchCount = prefetchCount;
        }

        public uint MaxRetryCount { get; }

        public Func<uint, TimeSpan> DelayFunc { get; }

        public ushort PrefetchCount { get; }
    }
}