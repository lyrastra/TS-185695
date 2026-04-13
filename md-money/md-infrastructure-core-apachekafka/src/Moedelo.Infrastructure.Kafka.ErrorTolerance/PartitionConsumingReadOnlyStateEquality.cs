namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

internal readonly record struct PartitionConsumingReadOnlyStateEquality(
    bool IsTheSame,
    bool IsNull1,
    bool IsNull2,
    bool AreTopicsSame,
    bool ArePartitionsSame,
    bool AreConsumerGroupIdsSame,
    bool AreCommittedOffsetsSame,
    bool AreOffsetMapDepthsSame,
    bool AreOffsetMapsEqual);
