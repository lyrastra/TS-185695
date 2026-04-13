using System.Collections.Concurrent;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

internal class PartitionConsumingState : IPartitionConsumingState
{
    public const int MaxOffsetMapSize = 1024 * 24;

    private int consumedMessagesCount = 0;

    // ReSharper disable once MemberCanBePrivate.Global
    internal PartitionConsumingState(string consumerGroupId, string topic, int partition, IOffsetMap offsetMap)
    {
        this.offsetMap = offsetMap;
        ConsumerGroupId = consumerGroupId;
        Topic = topic;
        Partition = partition;
    }

    public PartitionConsumingState(
        string consumerGroupId,
        string topic,
        int partition,
        int maxOffsetMapDepth)
        : this(consumerGroupId, topic, partition, new OffsetMap($@"{topic}[{partition}]", maxOffsetMapDepth))
    {
        ConsumerGroupId = consumerGroupId;
        Topic = topic;
        Partition = partition;
    }

    public string ConsumerGroupId { get; }
    public string Topic { get; }
    public int Partition { get; }
    public bool IsAssigned { get; private set; }
    public long? CommittedOffset => offsetMap.CommittedOffset;
    public DateTime? CommittedDateTimeUtc => offsetMap.CommittedDateTimeUtc;
    public int OffsetMapDepth => offsetMap.OffsetMapDepth;
    public byte[] OffsetMapData => offsetMap.OffsetMapData;
    private readonly IOffsetMap offsetMap;
    private readonly ConcurrentDictionary<string, Counter> skippedMessageKeys = new();

    public bool IsAlreadyProcessed(long offset)
    {
        return offsetMap.IsMarkedAsProcessed(offset);
    }

    public void CommitOffset(long offset, string messageKey)
    {
        var skippedCount = offsetMap.CountSkippedNotAfter(offset);

        if (skippedCount > 1)
        {
            throw new InvalidCommittedOffsetException(
                new KafkaTopicPartitionOffset(Topic, Partition, offset),
                $"До указанного смещения есть более одного пропущенного сообщения (пропущено: {skippedCount})");
        }

        if (skippedCount == 1)
        {
            DecrementSkippedMessagesByKey(messageKey);
        }
        
        offsetMap.SetCommittedOffset(offset);
    }

    public void MoveCommittedOffsetTo(long partitionOffsetOffset)
    {
        if (consumedMessagesCount != 1)
        {
            throw new InvalidCommittedOffsetException(
                new KafkaTopicPartitionOffset(Topic, Partition, partitionOffsetOffset),
                $"Перемещение закоммиченного смещения возможно только сразу после первого извлечения сообщения. Извлечено: {consumedMessagesCount}");
        }

        if (offsetMap.CommittedOffset.HasValue && partitionOffsetOffset <= offsetMap.CommittedOffset)
        {
            throw new InvalidCommittedOffsetException(
                new KafkaTopicPartitionOffset(Topic, Partition, partitionOffsetOffset),
                $"Перемещение закоммиченного смещения возможно только вперёд. Текущее значение: {offsetMap.CommittedOffset.Value}");
        }
        
        offsetMap.SetCommittedOffset(partitionOffsetOffset);
    }

    public void SetCommitmentState(long committedOffset, DateTime committedAtUtc, int offsetMapDepth, byte[] offsetMapData)
    {
        if (IsAssigned == false)
        {
            throw new InvalidOperationOnNonAssignedPartitionException(Topic, Partition);
        }

        offsetMap.Set(committedOffset, committedAtUtc, offsetMapDepth, offsetMapData);
    }

    public bool HasAnySkippedMessageBefore(long offset)
    {
        return offsetMap.HasAnySkippedMessageBefore(offset);
    }

    public bool HasAnySkippedMessageWithKey(string messageKey)
    {
        return skippedMessageKeys.TryGetValue(messageKey, out var counter) && counter.Value > 0;
    }

    public void MarkAsProcessed(long offset, string messageKey)
    {
        if (skippedMessageKeys.TryGetValue(messageKey, out var count) && count.Value > 0)
        {
            throw new InvalidMarkingOffsetException(
                new KafkaTopicPartitionOffset(Topic, Partition, offset),
                $"Нельзя пометить сообщение обработанным, поскольку уже есть одно и более пропущенное сообщение с таким же ключом {messageKey}");
        }
        
        offsetMap.MarkAsProcessed(offset);
    }

    public void MarkAsSkipped(long offset, string messageKey)
    {
        IncrementSkippedMessagesByKey(messageKey);

        offsetMap.MarkAsSkipped(offset);
    }

    public void Assigned()
    {
        if (IsAssigned)
        {
            throw new InvalidAssignOfAlreadyAssignedPartitionException(Topic, Partition);
        }

        IsAssigned = true;
    }

    public void Revoked()
    {
        if (IsAssigned == false)
        {
            throw new InvalidOperationOnNonAssignedPartitionException(Topic, Partition);
        }

        IsAssigned = false;
    }

    public int CountUniqueSkippedMessageKeys()
    {
        return skippedMessageKeys.Count;
    }

    public int IncrementConsumedMessageCount()
    {
        return Interlocked.Increment(ref consumedMessagesCount);
    }

    private void DecrementSkippedMessagesByKey(string messageKey)
    {
        if (skippedMessageKeys.TryGetValue(messageKey, out var count))
        {
            var newValue = count.Decrement();

            if (newValue < 0)
            {
                throw new InvalidOperationException(
                    $"Неожиданный нулевой счётчик пропущенных сообщения для {messageKey}");
            }
        }
    }

    private void IncrementSkippedMessagesByKey(string messageKey)
    {
        var counter = skippedMessageKeys.GetOrAdd(messageKey, _ => new Counter());
        counter.Increment();
    }
}
