using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests.Models;

internal sealed record PartitionConsumingReadOnlyState(
    string ConsumerGroupId,
    string Topic,
    int Partition,
    long? CommittedOffset,
    DateTime? CommittedDateTimeUtc,
    int OffsetMapDepth,
    byte[] OffsetMapData
) : IPartitionConsumingReadOnlyState
{}
