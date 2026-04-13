using System.Collections.Concurrent;
using System.Diagnostics;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Extensions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

internal sealed class ConsumingStateMemory : IConsumingStateMemory
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<int, IPartitionConsumingState>> topics = new();
    private readonly IPartitionConsumingStateFactory stateFactory;
    private readonly int maxOffsetMapDepth;

    public ConsumingStateMemory(string consumerGroupId, IPartitionConsumingStateFactory stateFactory, int maxOffsetMapDepth)
    {
        Debug.Assert(maxOffsetMapDepth > 0);

        this.ConsumerGroupId = consumerGroupId;
        this.stateFactory = stateFactory;
        this.maxOffsetMapDepth = maxOffsetMapDepth;
    }

    public string ConsumerGroupId { get; }

    public void Assigned(IPartitionConsumingReadOnlyState state)
    {
        var topicState = GetTopicState(state.Topic);

        var partitionState = stateFactory.Create(ConsumerGroupId, state.Topic, state.Partition, maxOffsetMapDepth);
        partitionState.Assigned();

        if (state.CommittedOffset.HasValue)
        {
            partitionState.SetCommitmentState(
                state.CommittedOffset.Value,
                state.CommittedDateTimeUtc.EnsureIsNotNull(nameof(state.CommittedDateTimeUtc))!.Value,
                state.OffsetMapDepth,
                state.OffsetMapData);
        }

        topicState[state.Partition] = partitionState;
    }

    public void Revoked(KafkaTopicPartitionOffset offset)
    {
        var topicState = GetTopicState(offset.Topic);
        if (topicState.TryRemove(offset.Partition, out var revoked))
        {
            revoked.Revoked();
        }
    }

    public IPartitionConsumingReadOnlyState CommitOffset(KafkaTopicPartitionOffset partitionOffset, string messageKey)
    {
        var partitionState = GetTopicPartitionState(partitionOffset.Topic, partitionOffset.Partition);

        if (partitionState.HasAnySkippedMessageBefore(partitionOffset.Offset))
        {
            throw new InvalidCommittedOffsetException(partitionOffset, reason: "В топике есть пропущенные сообщения до этого смещения");
        }

        partitionState.CommitOffset(partitionOffset.Offset, messageKey);

        return partitionState;
    }

    public IPartitionConsumingReadOnlyState MoveCommittedOffsetTo(KafkaTopicPartitionOffset partitionOffset)
    {
        var partitionState = GetTopicPartitionState(partitionOffset.Topic, partitionOffset.Partition);

        partitionState.MoveCommittedOffsetTo(partitionOffset.Offset);

        return partitionState;
    }

    public IPartitionConsumingReadOnlyState ProcessMessage(KafkaTopicPartitionOffset offset, string messageKey)
    {
        var partitionState = GetTopicPartitionState(offset.Topic, offset.Partition);
        partitionState.MarkAsProcessed(offset.Offset, messageKey);

        return partitionState;
    }

    public IPartitionConsumingReadOnlyState SkipMessage(KafkaTopicPartitionOffset offset, string messageKey)
    {
        var partitionState = GetTopicPartitionState(offset.Topic, offset.Partition);

        partitionState.MarkAsSkipped(offset.Offset, messageKey);

        return partitionState;
    }

    public IPartitionConsumingReadOnlyState GetPartitionState(KafkaTopicPartition partition)
    {
        return GetTopicPartitionState(partition.Topic, partition.Partition);
    }

    public int CountUniqueSkippedMessageKeys(KafkaTopicPartition partition)
    {
        return GetTopicPartitionState(partition.Topic, partition.Partition).CountUniqueSkippedMessageKeys();
    }

    public bool IsMessageAlreadyProcessed(KafkaTopicPartitionOffset offset)
    {
        var partitionState = GetTopicPartitionState(offset.Topic, offset.Partition);

        if (partitionState.CommittedOffset.HasValue == false)
        {
            // неизвестно, по умолчанию считаем, что сообщение ещё не обработано
            return false;
        }

        if (offset.Offset <= partitionState.CommittedOffset.Value)
        {
            // смещение сообщения имеет значение меньше или равное текущей позиции - оно уже обработано
            return true;
        }

        return partitionState.IsAlreadyProcessed(offset.Offset);
    }

    public bool HasNonEmptyOffsetMapMessage(KafkaTopicPartition partition)
    {
        var partitionState = GetTopicPartitionState(partition.Topic, partition.Partition);

        return partitionState.OffsetMapDepth > 0;
    }

    public bool HasAnySkippedMessageBefore(KafkaTopicPartitionOffset offset)
    {
        var partitionState = GetTopicPartitionState(offset.Topic, offset.Partition);

        return partitionState.HasAnySkippedMessageBefore(offset.Offset);
    }

    public bool HasAnySkippedMessageWithKey(KafkaTopicPartition offset, string messageKey)
    {
        var partitionState = GetTopicPartitionState(offset.Topic, offset.Partition);

        return partitionState.HasAnySkippedMessageWithKey(messageKey);
    }

    public PartitionConsumingSessionState MessageIsConsumed(KafkaTopicPartitionOffset offset, string messageKey)
    {
        var partitionState = GetTopicPartitionState(offset.Topic, offset.Partition);

        var consumedCount = partitionState.IncrementConsumedMessageCount();

        return new PartitionConsumingSessionState(consumedCount, partitionState.CommittedOffset);
    }

    private ConcurrentDictionary<int, IPartitionConsumingState> GetTopicState(string topic)
    {
        return topics.GetOrAdd(topic, static _ => new());
    }

    private IPartitionConsumingState GetTopicPartitionState(string topic, int partition)
    {
        var topicState = GetTopicState(topic);

        if (topicState.TryGetValue(partition, out var state))
        {
            return state;
        }

        throw new InvalidOperationOnNonAssignedPartitionException(topic, partition);
    }
}
