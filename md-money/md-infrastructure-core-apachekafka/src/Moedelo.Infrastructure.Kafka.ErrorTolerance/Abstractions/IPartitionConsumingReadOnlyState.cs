namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

public interface IPartitionConsumingReadOnlyState
{
    string ConsumerGroupId { get; }
    string Topic { get; }
    int Partition { get; }
    long? CommittedOffset { get; }
    DateTime? CommittedDateTimeUtc { get; }
    int OffsetMapDepth { get; }
    byte[] OffsetMapData { get; }
}
