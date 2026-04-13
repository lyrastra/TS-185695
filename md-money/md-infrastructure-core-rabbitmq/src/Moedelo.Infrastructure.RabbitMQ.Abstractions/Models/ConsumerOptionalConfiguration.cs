namespace Moedelo.Infrastructure.RabbitMQ.Abstractions.Models
{
    public sealed class ConsumerOptionalConfiguration
    {
        public ConsumerOptionalConfiguration(ushort prefetchCount = 50, int priority = 0, bool isExclusive = false)
        {
            PrefetchCount = prefetchCount;
            Priority = priority;
            IsExclusive = isExclusive;
        }

        public ushort PrefetchCount { get; }

        public int Priority { get; }

        public bool IsExclusive { get; }
    }
}