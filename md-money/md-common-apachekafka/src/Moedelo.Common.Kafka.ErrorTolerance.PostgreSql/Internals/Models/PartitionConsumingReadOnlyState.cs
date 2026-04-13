using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Models;

internal sealed record PartitionConsumingReadOnlyState(
    string ConsumerGroupId,
    string Topic,
    int Partition,
    long? CommittedOffset,
    DateTime? CommittedDateTimeUtc,
    int OffsetMapDepth,
    byte[] OffsetMapData
) : IPartitionConsumingReadOnlyState
{
}
