using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Extensions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

internal sealed class KafkaConsumerStateMemory : IKafkaConsumerStateMemory
{
    private readonly IKafkaConsumerMessageMemoryRepository repository;
    private readonly LazyStateLoadingMemoryWrapper memoryWrapper;

    public KafkaConsumerStateMemory(
        IKafkaConsumerMessageMemoryRepository repository,
        IConsumingStateMemory memory)
    {
        this.repository = repository;
        this.memoryWrapper = new LazyStateLoadingMemoryWrapper(repository, memory);
    }

    public async ValueTask<ConsumingSideEffect> MessageIsConsumedAsync(
        KafkaTopicPartitionOffset messageOffset,
        string messageKey,
        CancellationToken cancellationToken)
    {
        var memory = await memoryWrapper.GetMemoryAsync(cancellationToken);
        var sessionState = memory.MessageIsConsumed(messageOffset, messageKey);

        if (sessionState is { ConsumedMessagesCount: 1, CommittedOffset: { } })
        {
            var expectedCommittedOffset = messageOffset.Offset - 1;

            if (sessionState.CommittedOffset < expectedCommittedOffset)
            {
                // это первое полученное сообщение в рамках текущей сессии, есть сохраненное закоммиченное смещение
                // и смещение первого полученного сообщения больше того, что было сохранено
                // это означает, что смещение было изменено с момента последнего сохранения состояния.
                // (возможно, сообщение было "скипнуто" с использованием специального инструмента)
                // значит надо сместить сохранённое закоммиченное смещение до предшествующего текущему
                var prevOffset = messageOffset with { Offset = messageOffset.Offset - 1 };
                var partitionState = memory.MoveCommittedOffsetTo(prevOffset);

                await repository.SaveAsync(partitionState, cancellationToken);

                return new ConsumingSideEffect(
                    ConsumingSideEffect.SideEffect.CommittedOffsetHasBeenMoved,
                    partitionState);
            }
        }

        return new ConsumingSideEffect(ConsumingSideEffect.SideEffect.Nothing);
    }

    public async ValueTask<bool> IsMessageAlreadyProcessedAsync(KafkaTopicPartitionOffset messageOffset)
    {
        var memory = await memoryWrapper.GetMemoryAsync(CancellationToken.None);

        return memory.IsMessageAlreadyProcessed(messageOffset);
    }

    public async ValueTask SetCommittedOffsetAsync(KafkaTopicPartitionOffset messageOffset, string messageKey)
    {
        var memory = await memoryWrapper.GetMemoryAsync(CancellationToken.None);

        var hadAnySkippedMessage =
            memory.HasNonEmptyOffsetMapMessage(new KafkaTopicPartition(messageOffset.Topic, messageOffset.Partition));
        var partitionInfo = memory.CommitOffset(messageOffset, messageKey);

        if (hadAnySkippedMessage)
        {
            await repository.SaveAsync(partitionInfo, CancellationToken.None).ConfigureAwait(false);
        }
    }

    public async ValueTask MarkMessageAsSkippedAsync(KafkaTopicPartitionOffset messageOffset, string messageKey)
    {
        var memory = await memoryWrapper.GetMemoryAsync(CancellationToken.None);

        var partitionInfo = memory.SkipMessage(messageOffset, messageKey);

        await repository.SaveAsync(partitionInfo, CancellationToken.None).ConfigureAwait(false);
    }

    public async ValueTask MarkMessageAsProcessedAsync(KafkaTopicPartitionOffset messageOffset, string messageKey)
    {
        var memory = await memoryWrapper.GetMemoryAsync(CancellationToken.None);

        var hadSkippedMessages = memory.HasAnySkippedMessageBefore(messageOffset);
        var partitionInfo = memory.ProcessMessage(messageOffset, messageKey);

        if (hadSkippedMessages)
        {
            await repository.SaveAsync(partitionInfo, CancellationToken.None).ConfigureAwait(false);
        }
        // в противном случае нет необходимости сохранять состояние - в нём нет полезной информации
    }

    public async ValueTask<bool> HasSkippedMessagesBeforeAsync(KafkaTopicPartitionOffset messageOffset)
    {
        var memory = await memoryWrapper.GetMemoryAsync(CancellationToken.None);

        return memory.HasAnySkippedMessageBefore(messageOffset);
    }

    public async ValueTask<bool> HasAnySkippedMessageWithKeyAsync(KafkaTopicPartition partition, string messageKey)
    {
        var memory = await memoryWrapper.GetMemoryAsync(CancellationToken.None);

        return memory.HasAnySkippedMessageWithKey(partition, messageKey);
    }

    public void PartitionAssigned(KafkaTopicPartition partition)
    {
        memoryWrapper.AssignPartition(partition);
    }

    public void PartitionRevoked(KafkaTopicPartitionOffset partitionOffset)
    {
        memoryWrapper.RevokePartition(partitionOffset);
    }

    public async ValueTask<PartitionMemoryState> GetPartitionStateAsync(KafkaTopicPartition partition)
    {
        var memory = await memoryWrapper.GetMemoryAsync(CancellationToken.None);
        
        var partitionState = memory.GetPartitionState(partition);
        var uniqueKeysCount = memory.CountUniqueSkippedMessageKeys(partition);
        var committedOffset = partitionState.CommittedOffset.HasValue
            ? new CommittedOffsetInfo(
                partitionState.CommittedOffset.Value,
                partitionState.CommittedDateTimeUtc.EnsureIsNotNull(nameof(partitionState.CommittedDateTimeUtc))!.Value)
            : default;

        return new PartitionMemoryState(
            partition.Partition,
            committedOffset,
            partitionState.OffsetMapDepth,
            uniqueKeysCount
        );
    }
}
