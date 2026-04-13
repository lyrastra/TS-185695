using System.Collections.Concurrent;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.Kafka.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Exceptions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Extensions;
using Moedelo.Infrastructure.Kafka.Extensions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

internal sealed class ErrorToleratedKafkaConsumer : IKafkaConsumer
{
    private readonly ILogger logger;
    private readonly IKafkaConsumerStateMemory memory;
    private readonly IErrorToleratedKafkaConsumerOptions options;
    private readonly KafkaConsumerConfig config;
    private readonly IConsumer<string, string> rawConsumer;
    private readonly ConcurrentDictionary<string, ISet<int>> pausedTopicPartitions = new ();
    private readonly IPartitionStateEstimator partitionStateEstimator;
    private Error? lastFatalError;

    internal ErrorToleratedKafkaConsumer(
        KafkaConsumerConfig config,
        IErrorToleratedKafkaConsumerOptions options,
        IKafkaConsumerStateMemory memory,
        IPartitionStateEstimator partitionStateEstimator,
        IConfluentConsumerFactory confluentConsumerFactory,
        ILogger logger)
    {
        ConsumerUid = ConsumerUidGenerator.NextUid;
        this.options = options.EnsureIsNotNull(nameof(options));
        this.memory = memory.EnsureIsNotNull(nameof(memory));
        this.partitionStateEstimator = partitionStateEstimator;
        this.logger = logger;
        this.config = config;
        var consumerConfig = config.GetConsumerConfig();
        ConsumerGroupId = config.GroupId;
        MaxPollIntervalMs = consumerConfig.MaxPollIntervalMs;
        rawConsumer = confluentConsumerFactory.Create(consumerConfig,
            new ConfluentConsumerEventHandlers(OnError, OnPartitionAssigned, OnPartitionRevoked));
    }

    public void Dispose()
    {
        try
        {
            rawConsumer.Close();
            rawConsumer.Dispose();
        }
        catch
        {
            /* ничего */
        }
    }

    public int? MaxPollIntervalMs { get; }
    public bool CanConsume => lastFatalError == null;
    public KafkaConsumerGroupId ConsumerGroupId { get; }
    public string ConsumerUid { get; }

    public async ValueTask<IConsumeResultWrap> ConsumeAsync(CancellationToken cancellationToken)
    {
        const int maxConsumeExceptionCountInRow = 1000;
        var consumeExceptionCount = 0;

        while (CanConsume)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var consumeResult = rawConsumer.Consume(cancellationToken);

                var sideEffect = await memory
                    .MessageIsConsumedAsync(
                        consumeResult.TopicPartitionOffset.ToDomain(),
                        consumeResult.Message.Key,
                        cancellationToken)
                    .ConfigureAwait(false);

                if (sideEffect.Effect != ConsumingSideEffect.SideEffect.Nothing)
                {
                    logger.LogConsumingSideEffect(sideEffect.PartitionState!);
                }

                var isHandled = await AutoHandleByMemoryReasonsAsync(consumeResult).ConfigureAwait(false);

                if (isHandled)
                {
                    var partitionState = await memory.GetPartitionStateAsync(consumeResult.TopicPartition.ToDomain());
                    var memoryStatus = partitionStateEstimator.EstimateMemoryStatus(options, partitionState, consumeResult.Offset.Value);

                    if (memoryStatus.IsReadOnly)
                    {
                        logger.LogErrorPartitionFinallySetOnPauseDueToMemoryIsReadOnly(
                            consumeResult.TopicPartitionOffset.ToDomain(),
                            memoryStatus.ReadOnlyReason);

                        rawConsumer.Pause(new [] {consumeResult.TopicPartition});
                    }

                    continue;
                }

                return new ConsumeResultWrapImpl(consumeResult);
            }
            catch (ConsumeException exception)
            {
                // такие исключения игнорируем: фатальные обработаются через обработчик SetErrorHandler выставлением CanConsume = false,
                // в остальных случаях библиотека сама попробует восстановить работоспособность
                if (++consumeExceptionCount >= maxConsumeExceptionCountInRow)
                {
                    throw new TooManyConsumeExceptionsException(consumeExceptionCount, exception);
                }
            }
        }

        if (lastFatalError == null)
        {
            throw new ConsumerFatalException("Неизвестная ошибка", "UnknownError", 404);
        }

        throw new ConsumerFatalException(
            lastFatalError.Reason,
            lastFatalError.Code.ToString(),
            (int)lastFatalError.Code);
    }

    private async ValueTask<bool> AutoHandleByMemoryReasonsAsync(ConsumeResult<string, string> consumeResult)
    {
        var topicPartitionOffset = consumeResult.TopicPartitionOffset.ToDomain();
        var topicPartition = consumeResult.TopicPartition.ToDomain();
        var messageKey = consumeResult.Message.Key;

        var hasAnySkippedMessages = await memory.HasAnySkippedMessageWithKeyAsync(topicPartition, messageKey); 

        if (hasAnySkippedMessages)
        {
            logger.LogMessageSkippingChainStopReason(topicPartitionOffset, messageKey);
            // это сообщение нельзя обработать - одно из предыдущих сообщений с таким же ключом уже было отброшено при обработке
            await memory.MarkMessageAsSkippedAsync(topicPartitionOffset, messageKey).ConfigureAwait(false);

            return true;
        }

        var isAlreadyProcessed = await memory.IsMessageAlreadyProcessedAsync(topicPartitionOffset); 

        if (isAlreadyProcessed)
        {
            logger.LogMessageSkippingAsAlreadyProcessed(topicPartitionOffset, messageKey);

            var hasSkippedMessagesBefore = await memory.HasSkippedMessagesBeforeAsync(topicPartitionOffset); 

            if (hasSkippedMessagesBefore == false)
            {
                await DoCommitAsync(new ConsumeResultWrapImpl(consumeResult)).ConfigureAwait(false);
            }

            return true;
        }

        return false;
    }

    public void Subscribe(string topicName)
    {
        logger.LogSubscribe(ConsumerGroupId.IdInKafka, topicName);
        rawConsumer.Subscribe(topicName);
    }

    public async ValueTask CommitAsync(IConsumeResultWrap consumeResult)
    {
        var hasSkippedMessagesBefore = await memory.HasSkippedMessagesBeforeAsync(consumeResult.TopicPartitionOffset);
        
        if (hasSkippedMessagesBefore)
        {
            // отмечаем сообщение как обработанное, но смещение не коммитим в кафку
            await memory
                .MarkMessageAsProcessedAsync(consumeResult.TopicPartitionOffset, consumeResult.MessageKey!)
                .ConfigureAwait(false);

            logger.LogMarkMessageAsProcessed(consumeResult);

            return;
        }

        await DoCommitAsync(consumeResult).ConfigureAwait(false);
    }

    private async ValueTask DoCommitAsync(IConsumeResultWrap consumeResult)
    {
        var rawConsumeResult = consumeResult.ToRawConsumeResult();
        rawConsumer!.Commit(rawConsumeResult);
        await memory.SetCommittedOffsetAsync(consumeResult.TopicPartitionOffset, consumeResult.MessageKey!)
            .ConfigureAwait(false);
        logger.LogOffsetCommitted(consumeResult);
    }

    public bool IsPaused(KafkaTopicPartitionOffset topicPartition)
    {
        var pausedPartitions = pausedTopicPartitions.GetOrAdd(topicPartition.Topic, _ => new HashSet<int>());

        return pausedPartitions.Contains(topicPartition.Partition);
    }

    public async ValueTask OnMessageHandlingFailedAsync(KafkaTopicPartitionOffset topicPartitionOffset, string? messageKey)
    {
        await memory.MarkMessageAsSkippedAsync(topicPartitionOffset, messageKey ?? string.Empty).ConfigureAwait(false);
        logger.LogPartitionSetOnPause(topicPartitionOffset, messageKey);

        pausedTopicPartitions
            .GetOrAdd(topicPartitionOffset.Topic, _ => new HashSet<int>())
            .Add(topicPartitionOffset.Partition);
    }

    private void OnError(IConsumer<string, string> consumer, Error error)
    {
        lastFatalError ??= error.IsFatal ? error : null;

        logger.LogConsumerError(config, error);
    }

    private void OnPartitionAssigned(IConsumer<string, string> consumer, List<TopicPartition> partitions)
    {
        foreach (var partition in partitions)
        {
            logger.LogOnPartitionAssigned(ConsumerGroupId, partition);

            try
            {
                memory.PartitionAssigned(partition.ToDomain());
            }
            catch (Exception exception)
            {
                logger.LogOnPartitionAssignedError(ConsumerGroupId, exception, partition);
                throw;
            }
        }
    }

    private void OnPartitionRevoked(IConsumer<string, string> consumer, List<TopicPartitionOffset> partitions)
    {
        foreach (var offset in partitions)
        {
            logger.LogOnPartitionRevoked(ConsumerGroupId, offset);

            try
            {
                memory.PartitionRevoked(offset.ToDomain());
            }
            catch (Exception exception)
            {
                logger.LogOnPartitionRevokedError(ConsumerGroupId, exception, offset);
                throw;
            }
        }
    }
}
