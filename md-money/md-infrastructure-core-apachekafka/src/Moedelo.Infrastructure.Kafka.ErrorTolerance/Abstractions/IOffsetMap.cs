namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

internal interface IOffsetMap
{
    long? CommittedOffset { get; }
    DateTime? CommittedDateTimeUtc { get; }
    int OffsetMapDepth { get; }
    byte[] OffsetMapData { get; }
    void Set(long committedOffset, DateTime committedAtUtc, int offsetMapDepth, byte[] offsetMapData);
    void SetCommittedOffset(long offset);
    void MarkAsSkipped(long offset);
    void MarkAsProcessed(long offset);
    bool IsMarkedAsProcessed(long offset);
    int CountSkippedNotAfter(long offset);
    bool HasAnySkippedMessageBefore(long offset);
}
