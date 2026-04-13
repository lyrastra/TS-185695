using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.Kafka.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Exceptions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Extensions;

namespace Moedelo.Infrastructure.Kafka;

internal class KafkaConsumerImpl : IKafkaConsumer
{
    private const int DefaultMaxConsumeExceptionCountInRow = 1000;

    private IConsumer<string, string>? rawConsumer;
    private readonly KafkaConsumerConfig config;
    private readonly ConcurrentDictionary<string, ISet<int>> pausedTopicPartitions = new ();
    private Error? lastFatalError;
    private readonly int maxConsumeExceptionCountInRow;
    private readonly ILogger logger;

    internal KafkaConsumerImpl(KafkaConsumerConfig config,
        IConfluentConsumerFactory confluentConsumerFactory,
        ILogger logger)
    {
        this.logger = logger;
        this.config = config;
        ConsumerUid = ConsumerUidGenerator.NextUid;
        var consumerConfig = config.GetConsumerConfig();
        ConsumerGroupId = config.GroupId;
        MaxPollIntervalMs = consumerConfig.MaxPollIntervalMs;
        maxConsumeExceptionCountInRow = config.MaxCountOfIgnoringConsumeExceptionsInRow ?? DefaultMaxConsumeExceptionCountInRow;

        rawConsumer = confluentConsumerFactory.Create(consumerConfig,
            new ConfluentConsumerEventHandlers(OnError, OnPartitionAssigned, OnPartitionRevoked));
    }

    public int? MaxPollIntervalMs { get; }
    public bool CanConsume => lastFatalError == null;

    public KafkaConsumerGroupId ConsumerGroupId { get; }
    public string ConsumerUid { get; }

    public async ValueTask<IConsumeResultWrap> ConsumeAsync(CancellationToken cancellationToken)
    {
        var consumeExceptionCount = 0;

        while (CanConsume)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var consumeResult = rawConsumer!.Consume(cancellationToken);

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
            await Task.Delay(TimeSpan.FromMilliseconds(consumeExceptionCount), cancellationToken);
        }

        if (lastFatalError == null)
        {
            throw new ConsumerFatalException();
        }

        throw new ConsumerFatalException(
            lastFatalError.Reason,
            lastFatalError.Code.ToString(),
            (int)lastFatalError.Code);
    }

    public void Subscribe(string topicName)
    {
        logger.LogSubscribe(ConsumerGroupId.IdInKafka, topicName);
        rawConsumer!.Subscribe(topicName);
    }

    public void Dispose()
    {
        try
        {
            rawConsumer?.Close();
            rawConsumer?.Dispose();
            rawConsumer = null;
        }
        catch
        {
            /* ничего */
        }
    }

    public ValueTask CommitAsync(IConsumeResultWrap consumeResult)
    {
        var rawConsumeResult = consumeResult.ToRawConsumeResult();
        
        rawConsumer!.Commit(rawConsumeResult);

        return default;
    }

    public bool IsPaused(KafkaTopicPartitionOffset topicPartition)
    {
        var pausedPartitions = pausedTopicPartitions.GetOrAdd(topicPartition.Topic, _ => new HashSet<int>());

        return pausedPartitions.Contains(topicPartition.Partition);
    }

    public ValueTask OnMessageHandlingFailedAsync(KafkaTopicPartitionOffset topicPartition, string? messageKey)
    {
        var pausedPartitions = pausedTopicPartitions.GetOrAdd(topicPartition.Topic, _ => new HashSet<int>());

        if (pausedPartitions.Contains(topicPartition.Partition))
        {
            return default;
        }

        var rawTopicPartition = new TopicPartition(topicPartition.Topic, topicPartition.Partition);
        rawConsumer!.Pause(new [] { rawTopicPartition });
        pausedPartitions.Add(topicPartition.Partition);

        return default;
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
        }
    }

    private void OnPartitionRevoked(IConsumer<string, string> consumer, List<TopicPartitionOffset> partitions)
    {
        foreach (var offset in partitions)
        {
            logger.LogOnPartitionRevoked(ConsumerGroupId, offset);
        }
    }
}
