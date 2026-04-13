using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

public readonly record struct ConsumingSideEffect(
    ConsumingSideEffect.SideEffect Effect,
    IPartitionConsumingReadOnlyState? PartitionState = default)
{
    public enum SideEffect
    {
        Nothing = 0,
        CommittedOffsetHasBeenMoved = 1
    }
};
