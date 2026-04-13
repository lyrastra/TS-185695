namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

public interface IPartitionConsumingState : IPartitionConsumingReadOnlyState
{
    bool IsAlreadyProcessed(long offset);
    void CommitOffset(long offset, string messageKey);
    void MoveCommittedOffsetTo(long partitionOffsetOffset);
    void SetCommitmentState(long committedOffset, DateTime committedAtUtc, int offsetMapDepth, byte[] offsetMapData);
    bool HasAnySkippedMessageBefore(long offset);
    bool HasAnySkippedMessageWithKey(string messageKey);
    void MarkAsProcessed(long offset, string messageKey);
    void MarkAsSkipped(long offset, string messageKey);
    void Assigned();
    void Revoked();
    int CountUniqueSkippedMessageKeys();
    /// <summary>
    /// Увеличивает на 1 количество забранных из секции сообщений
    /// </summary>
    /// <returns></returns>
    int IncrementConsumedMessageCount();
}
