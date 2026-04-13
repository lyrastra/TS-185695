using System;
using System.Collections.Generic;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Internals;

namespace Moedelo.Infrastructure.Kafka.Extensions;

public static partial class LoggerExtensions
{
    #region LoggerMessage
    /// <summary>
    /// Логирование установки секции на паузу (для случая, когда в приложении одна группа консьюмеров)
    /// </summary>
    [LoggerMessage(EventId = 7005, Level = LogLevel.Error, Message = "{offsetInfo}: Partition set on pause")]
    private static partial void LogPartitionSetOnPauseError(this ILogger logger, Exception exception, string offsetInfo);

    [LoggerMessage(EventId = 7006, SkipEnabledCheck = true, Message = "{offset}: сообщение обработано за {duration} ms")]
    private static partial void LogMessageCommitment(this ILogger logger, LogLevel level, string offset, long duration);
    
    [LoggerMessage(EventId = 7031, Level = LogLevel.Error,
        Message = @"Сообщение обрабатывалось дольше максимально заданного времени: {actualDurationMs} > {maxPollIntervalMs}
Топик[секция]@смещение={topicInfo}
Группа консьюмеров={consumerGroupId}
Скорее всего, при коммите произойдёт критическая ошибка (ищи по логам ниже что-то типа Confluent.Kafka.KafkaException: Broker: Unknown member).
Как исправить:
(1) оптимизировать обработку = сократить время обработки
(2) увеличить таймаут MaxPollIntervalMs при конфигурировании readerBuilder'а.
См. Moedelo.Common.Kafka.Abstractions.Entities.OptionalReadSettings::MaxPollIntervalMs
и Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders.IMoedeloEntityCommandKafkaTopicReaderBuilder::WithOptionalSettings")]
    private static partial void LogErrorIfMaxPollIntervalExceeded(this ILogger logger,
        string consumerGroupId, int actualDurationMs, int maxPollIntervalMs, string topicInfo);

    [LoggerMessage(EventId = 7032, Message = "{topicName} => {consumerGroupId}: Consumer Error '{error}', Code {errorCode}")]
    private static partial void LogConsumerError(this ILogger logger,
        LogLevel logLevel, string consumerGroupId, string topicName, Error error, ErrorCode errorCode);

    [LoggerMessage(EventId = 7033, Level = LogLevel.Debug,
        Message = "{topicName}: консьюмер группы {ConsumerGroupId} подписан на топик")]
    public static partial void LogSubscribe(this ILogger logger, string consumerGroupId,
        string topicName);

    [LoggerMessage(EventId = 7034, Level = LogLevel.Error,
        Message = "{offsetInfo} => {consumerGroupId}: Failed commitment of message. ElapsedMilliseconds={totalMilliseconds}")]
    private static partial void LogMessageCommitmentError(this ILogger logger,
        Exception exception, string consumerGroupId, string offsetInfo, long totalMilliseconds);

    /// <summary>
    /// Логирование установки секции на паузу (для случая, когда в приложении более чем одна группа консьюмеров)
    /// </summary>
    [LoggerMessage(EventId = 7035, Level = LogLevel.Error,
        Message = "{offsetInfo} => {consumerGroupId}: Partition set on pause")]
    private static partial void LogPartitionSetOnPauseError(this ILogger logger,
        Exception exception, string consumerGroupId, string offsetInfo);

    [LoggerMessage(EventId = 7036, SkipEnabledCheck = true,
        Message = "{offset} => {consumerGroupId}: сообщение обработано за {duration} ms")]
    private static partial void LogMessageCommitment(this ILogger logger,
        LogLevel level, string consumerGroupId, string offset, long duration);

    [LoggerMessage(EventId = 7037, Level = LogLevel.Debug,
        Message = "{Topic}[{Partition}]: консьюмеру группы {consumerGroupId} назначена секция {Partition}")]
    private static partial void LogOnPartitionAssigned(this ILogger logger,
        string consumerGroupId, string topic, int partition);

    [LoggerMessage(EventId = 7038, Level = LogLevel.Debug,
        Message = "{TopicOffset}: у консьюмера группы {consumerGroupId} отозвана секция {Partition}")]
    private static partial void LogOnPartitionRevoked(this ILogger logger,
        string consumerGroupId, string topicOffset, int partition);

    [LoggerMessage(EventId = 7039, SkipEnabledCheck = true,
        Message = "{TopicName} => {ConsumerGroupId}: ожидание сообщения из топика")]
    private static partial void LogMessageConsuming(this ILogger logger, LogLevel level, string topicName, string consumerGroupId);

    [LoggerMessage(EventId = 7040, SkipEnabledCheck = true,
        Message = "{offsetInfo} => {ConsumerGroupId}: начало обработки сообщения")]
    private static partial void LogMessageConsumed(this ILogger logger, LogLevel level, string offsetInfo, string consumerGroupId);

    [LoggerMessage(EventId = 7041, Level = LogLevel.Error,
        Message = "{offsetInfo} => {ConsumerGroupId}: обработка сообщения была отменена")]
    private static partial void LogMessageHandlingCanceled(this ILogger logger, Exception exception,
        string offsetInfo, string consumerGroupId);
    
    [LoggerMessage(EventId = 7042, Level = LogLevel.Error,
        Message = "{TopicName} => {ConsumerGroupId}: caught KafkaException, error.code={ErrorCode}")]
    private static partial void LogKafkaException(this ILogger logger, Exception exception,
        string topicName, string consumerGroupId, ErrorCode? errorCode);

    #endregion

    internal static void LogMessageConsuming(this ILogger logger,
        LogLevel logLevel, string topicName, IKafkaConsumer kafkaConsumer)
    {
        if (logger.IsEnabled(logLevel) == false)
        {
            return;
        }

        using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", new { ConsumerId = kafkaConsumer.ConsumerUid })))
        {
            logger.LogMessageConsuming(logLevel, kafkaConsumer.ConsumerGroupId.IdInKafka, topicName);
        }
    }

    internal static void LogErrorIfMaxPollIntervalExceeded(this ILogger logger,
        int? maxPollIntervalMs, TimeSpan duration, IConsumeResultWrap consumeResult, IKafkaConsumer consumer)
    {
        if (duration.TotalMilliseconds > maxPollIntervalMs)
        {
            logger.LogErrorIfMaxPollIntervalExceeded(
                consumer.ConsumerGroupId.IdInKafka,
                (int)duration.TotalMilliseconds,
                maxPollIntervalMs.Value,
                consumeResult.OffsetLogPresentation());
        }
    }

    internal static void LogMessageConsumed<TMessage>(this ILogger logger,
        LogLevel logLevel, IConsumeResultWrap consumeResult, IKafkaConsumer kafkaConsumer)
    {
        if (!logger.IsEnabled(logLevel))
            return;

        using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData",
                   new { MessageValue = consumeResult.MessageValue!.FromJsonStringSafeOrDefault<TMessage>(), ConsumerId = kafkaConsumer.ConsumerUid })))
        {
            logger.LogMessageConsumed(logLevel, consumeResult.OffsetLogPresentation(), kafkaConsumer.ConsumerGroupId.IdInKafka);
        }
    }

    internal static void LogMessageCommitment(this ILogger logger,
        LogLevel logLevel,
        IConsumeResultWrap consumeResult,
        TimeSpan consumingDuration,
        IKafkaConsumer consumer)
    {
        if (!logger.IsEnabled(logLevel))
            return;

        using (logger.BeginScope(new KeyValuePair<string, string>("ExtraData", consumeResult.MessageValue!)))
        {
            if (ConsumerGroupIdStats.DoesApplicationHaveMultipleGroups)
            {
                logger.LogMessageCommitment(
                    logLevel,
                    consumer.ConsumerGroupId.IdInKafka,
                    consumeResult.OffsetLogPresentation(),
                    (long)consumingDuration.TotalMilliseconds); 
            }
            else
            {
                logger.LogMessageCommitment(
                    logLevel,
                    consumeResult.OffsetLogPresentation(),
                    (long)consumingDuration.TotalMilliseconds);
            }
        }
    }

    internal static void LogMessageCommitmentError(this ILogger logger,
        IConsumeResultWrap consumeResult,
        Exception exception,
        TimeSpan consumingDuration,
        IKafkaConsumer consumer)
    {
        using (logger.BeginScope(new KeyValuePair<string, string>("ExtraData", consumeResult.MessageValue!)))
        {
            logger.LogMessageCommitmentError(exception,
                consumer.ConsumerGroupId.IdInKafka,
                consumeResult.OffsetLogPresentation(),
                (long)consumingDuration.TotalMilliseconds); 
        }
    }

    public static void LogConsumerError(this ILogger logger, KafkaConsumerConfig config, Error error)
    {
        var groupId = config.GroupId.IdInKafka;
        var topicName = config.TopicName.NameInKafka;
        var errorCode = error.Code;

        if (error.IsFatal)
        {
            // чтобы сообщение находилось в кибане по фильтру Error
            logger.LogConsumerError(LogLevel.Error, groupId, topicName, error, errorCode);
            // чтобы сообщение находилось в кибане по фильтру Critical
            logger.LogConsumerError(LogLevel.Critical, groupId, topicName, error, errorCode);
        }
        else
        {
            logger.LogConsumerError(LogLevel.Warning, groupId, topicName, error, errorCode);
        }
    }

    public static void LogOnPartitionAssigned(this ILogger logger,
        KafkaConsumerGroupId groupId, TopicPartition topicPartition) =>
        logger.LogOnPartitionAssigned(groupId.IdInKafka, topicPartition.Topic, topicPartition.Partition.Value);

    public static void LogOnPartitionRevoked(this ILogger logger,
        KafkaConsumerGroupId groupId, TopicPartitionOffset offset) =>
        logger.LogOnPartitionRevoked(groupId.IdInKafka, offset.ToShortString(), offset.Partition.Value);

    internal static void LogPartitionSetOnPauseError(this ILogger logger,
        IConsumeResultWrap consumeResult,
        Exception exception, IKafkaConsumer kafkaConsumer)
    {
        using (logger.BeginScope(new KeyValuePair<string, string>("ExtraData", consumeResult.MessageValue!)))
        {
            if (ConsumerGroupIdStats.DoesApplicationHaveMultipleGroups)
            {
                logger.LogPartitionSetOnPauseError(exception,
                    kafkaConsumer.ConsumerGroupId.IdInKafka, consumeResult.OffsetLogPresentation());
            }
            else
            {
                logger.LogPartitionSetOnPauseError(exception,
                    consumeResult.OffsetLogPresentation());
            }
        }
    }

    internal static void LogMessageHandlingCanceled(this ILogger logger,
        IConsumeResultWrap consumeResult, Exception exception, IKafkaConsumer kafkaConsumer)
    {
        using (logger.BeginScope(new KeyValuePair<string, string>("ExtraData", consumeResult.MessageValue!)))
        {
            logger.LogMessageHandlingCanceled(exception,
                consumeResult.OffsetLogPresentation(),
                kafkaConsumer.ConsumerGroupId.IdInKafka);
        }
    }

    internal static void LogKafkaException(this ILogger logger,
        KafkaConsumerConfig settings, KafkaException exception, KafkaConsumerGroupId consumerGroupId)
    {
        logger.LogKafkaException(exception, settings.TopicName.NameInKafka, consumerGroupId.IdInKafka, exception.Error?.Code);
    }
}
