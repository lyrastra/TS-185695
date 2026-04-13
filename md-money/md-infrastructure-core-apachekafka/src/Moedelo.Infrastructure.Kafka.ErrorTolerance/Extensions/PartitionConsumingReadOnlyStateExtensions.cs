using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Extensions;

internal static class PartitionConsumingReadOnlyStateExtensions
{
    internal static string ToPartitionString(this IPartitionConsumingReadOnlyState state)
    {
        return $"{state.Topic}[{state.Partition}]";
    }

    internal static string ToPartitionOffsetString(this IPartitionConsumingReadOnlyState state)
    {
        return $"{state.Topic}[{state.Partition}]@{state.CommittedOffset}";
    }
}
