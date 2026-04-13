using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.Kafka.Extensions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(EventId = 7204, Level = LogLevel.Error,
        Message = "{Topic}[{Partition}]: ошибка при назначении секции консьюмеру группы {ConsumerGroupId}")]
    private static partial void LogOnPartitionAssignedError(
        this ILogger logger,
        Exception exception,
        string consumerGroupId,
        string topic,
        int partition);

    [LoggerMessage(EventId = 7205, Level = LogLevel.Error,
        Message = "{topicOffset}: ошибка при отзыве секции у консьюмера группы {ConsumerGroupId}")]
    private static partial void LogOnPartitionRevokedError(
        this ILogger logger,
        Exception exception,
        string consumerGroupId,
        string topicOffset);

    [LoggerMessage(EventId = 7206, Level = LogLevel.Information,
        Message = "{offset}#{messageKey}: обработка сообщения пропускается. Причина: уже есть пропущенные необработанные сообщения с таким же ключом")]
    private static partial void LogMessageSkippingChainStopReason(
        this ILogger logger,
        string offset,
        string messageKey);

    [LoggerMessage(EventId = 7207, Level = LogLevel.Information,
        Message = "{offset}#{messageKey}: обработка сообщения пропускается. Причина: это сообщение уже было обработано ранее")]
    private static partial void LogMessageSkippingAsAlreadyProcessed(
        this ILogger logger, string offset, string messageKey);

    [LoggerMessage(EventId = 7208, Level = LogLevel.Error,
        Message = "{offset}#{messageKey}: обработка сообщения пропускается. Причина: ошибка при обработке сообщения. Обработка сообщений топика будет продолжена без коммита смещения консьюмера в топике")]
    private static partial void LogPartitionSetOnPause(
        this ILogger logger, string offset, string? messageKey);

    [LoggerMessage(EventId = 7209, Level = LogLevel.Debug,
        Message = "{offset}#{messageKey}: сообщение отмечено как обработанное (без коммита)")]
    private static partial void LogMarkMessageAsProcessed(
        this ILogger logger, string offset, string? messageKey);

    [LoggerMessage(EventId = 7210, Level = LogLevel.Debug,
        Message = "{offset}#{messageKey}: сообщение отмечено как обработанное (с коммитом)")]
    private static partial void LogOffsetCommitted(
        this ILogger logger, string offset, string? messageKey);

    [LoggerMessage(EventId = 7211, Level = LogLevel.Error,
        Message = "{offset}: обработка секции окончательно остановлена. Причина: {reason}")]
    private static partial void LogErrorPartitionFinallySetOnPauseDueToMemoryIsReadOnly(
        this ILogger logger, string offset, string reason);

    [LoggerMessage(EventId = 7212, Level = LogLevel.Information,
        Message =
            "{offset}: обнаружен скачок закоммиченного смещения. Сохранённое состояние продвинуто до реального значения")]
    private static partial void LogConsumingSideEffect(this ILogger logger, string offset);

    internal static void LogOnPartitionAssignedError(this ILogger logger,
        KafkaConsumerGroupId consumerGroupId, Exception exception, TopicPartition topicPartition) =>
        logger.LogOnPartitionAssignedError(exception, consumerGroupId.IdInKafka, topicPartition.Topic, topicPartition.Partition.Value);

    internal static void LogOnPartitionRevokedError(this ILogger logger,
        KafkaConsumerGroupId consumerGroupId, Exception exception, TopicPartitionOffset offset) =>
        logger.LogOnPartitionRevokedError(exception, consumerGroupId.IdInKafka, offset.ToShortString());

    internal static void LogMessageSkippingChainStopReason(this ILogger logger,
        KafkaTopicPartitionOffset offset,
        string messageKey) => logger.LogMessageSkippingChainStopReason(offset.ToShortString(), messageKey);

    internal static void LogMessageSkippingAsAlreadyProcessed(this ILogger logger, KafkaTopicPartitionOffset offset,
        string messageKey) =>
        logger.LogMessageSkippingAsAlreadyProcessed(offset.ToShortString(), messageKey);

    internal static void LogPartitionSetOnPause(this ILogger logger, KafkaTopicPartitionOffset topicPartitionOffset, string? messageKey)
        => logger.LogPartitionSetOnPause(topicPartitionOffset.ToShortString(), messageKey);

    internal static void LogErrorPartitionFinallySetOnPauseDueToMemoryIsReadOnly(this ILogger logger,
        KafkaTopicPartitionOffset offset, string reason) =>
        logger.LogErrorPartitionFinallySetOnPauseDueToMemoryIsReadOnly(offset.ToShortString(), reason);

    internal static void LogConsumingSideEffect(this ILogger logger, IPartitionConsumingReadOnlyState partitionState)
        => logger.LogConsumingSideEffect(partitionState.ToPartitionOffsetString());
    
    internal static void LogMarkMessageAsProcessed(this ILogger logger, IConsumeResultWrap consumeResult)
    {
        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogMarkMessageAsProcessed(
                consumeResult.TopicPartitionOffset.ToShortString(),
                consumeResult.MessageKey);
        }
    }

    internal static void LogOffsetCommitted(this ILogger logger, IConsumeResultWrap consumeResult)
    {
        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogOffsetCommitted(
                consumeResult.TopicPartitionOffset.ToShortString(),
                consumeResult.MessageKey);
        }
    }
}
