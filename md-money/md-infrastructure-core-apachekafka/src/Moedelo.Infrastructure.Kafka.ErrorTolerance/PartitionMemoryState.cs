namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

public readonly record struct CommittedOffsetInfo(
    long Offset, DateTime AtUtc);

public readonly record struct PartitionMemoryState(
    int Partition,
    CommittedOffsetInfo CommittedOffset,
    int OffsetMapDepth,
    int UniqueSkippedMessageKeysCount);
