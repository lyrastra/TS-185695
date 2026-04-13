namespace Moedelo.InfrastructureV2.Domain.Models.EventBus;

public abstract class ConsumerOptionsBase
{
    protected ConsumerOptionsBase(ushort prefetchCount = 10)
    {
        PrefetchCount = prefetchCount;
    }

    public ushort PrefetchCount { get; }
}