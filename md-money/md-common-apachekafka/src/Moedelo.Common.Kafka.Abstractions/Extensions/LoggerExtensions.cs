#nullable enable
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(EventId = 77001, Level = LogLevel.Error, SkipEnabledCheck = true,
        Message = "{offset}: ошибка при обработке сообщения. Все попытки исчерпаны, попыток сделано {retryCount}")]
    private static partial void LogErrorInRetryHandler(this ILogger logger, string offset, Exception exception, int retryCount);
    
    [LoggerMessage(EventId = 77002, Level = LogLevel.Error, SkipEnabledCheck = true,
        Message = "{offset}: ошибка при обработке сообщения. Запланирована повторная попытка через {timeout:g}. Попыток сделано {retryCount}")]
    private static partial void LogErrorButRetryScheduled(this ILogger logger, string offset, Exception exception, TimeSpan timeout, int retryCount);

    [LoggerMessage(EventId = 77003, Level = LogLevel.Debug, SkipEnabledCheck = true,
        Message = "{offset}: начало обработки сообщения {messageId} от {messageCreateTime}")]
    private static partial void LogMessageProcessingIsStarting(
        this ILogger logger,
        string offset,
        DateTime? messageCreateTime,
        Guid? messageId);

    [LoggerMessage(EventId = 77004, Level = LogLevel.Debug, SkipEnabledCheck = true,
        Message = "{offset}: сообщение {messageId} от {messageCreateTime} обработано")]
    private static partial void LogMessageProcessingIsDone(
        this ILogger logger,
        string offset,
        DateTime? messageCreateTime,
        Guid? messageId);

    [LoggerMessage(EventId = 77005, Level = LogLevel.Trace, SkipEnabledCheck = true,
        Message = "{offset}: дамп сообщения {messageId}")]
    private static partial void LogDumpProcessingMessage(
        this ILogger logger,
        string offset,
        Guid? messageId);

    [LoggerMessage(EventId = 77006, Level = LogLevel.Error, 
        Message = "Неожиданный тип сообщения: получен {actualType}, когда ожидается только {expectedType}")]
    internal static partial void LogUnexpectedMessageType(this ILogger logger,
        string actualType, string expectedType);

    [LoggerMessage(EventId = 77007, Level = LogLevel.Debug, 
        Message = "Обработка топика {topicName}: пропускается сообщение неизвестного типа {messageType}")]
    internal static partial void LogSkipUnexpectedMessageType(this ILogger logger,
        string topicName, string messageType);

    [LoggerMessage(EventId = 77008, Level = LogLevel.Error, 
        Message = "{consumerType} не может быть запущен - произошло исключение")]
    internal static partial void LogUnableToStartConsumerError(this ILogger logger,
        Exception exception, string consumerType);
    
    internal static void LogMessageProcessingIsStarting(
        this ILogger logger, string topicName, KafkaMessageValueMetadata? metadata)
    {
        if (logger?.IsEnabled(LogLevel.Debug) == true)
        {
            logger.LogMessageProcessingIsStarting(
                $"{topicName}[{metadata?.Partition}]@{metadata?.Offset}",
                metadata?.MessageDate,
                metadata?.MessageId);
        }
    }

    internal static void LogMessageProcessingIsDone(
        this ILogger logger, string topicName, KafkaMessageValueMetadata? metadata)
    {
        if (logger?.IsEnabled(LogLevel.Debug) == true)
        {
            logger.LogMessageProcessingIsDone(
                $"{topicName}[{metadata?.Partition}]@{metadata?.Offset}",
                metadata?.MessageDate,
                metadata?.MessageId);
        }
    }

    internal static void LogDumpProcessingMessage<TMessage>(
        this ILogger logger,
        string topicName,
        KafkaMessageValueMetadata? metadata,
        TMessage message) where TMessage: class
    {
        if (logger?.IsEnabled(LogLevel.Trace) == true)
        {
            using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", message)))
            {
                logger.LogDumpProcessingMessage(
                    $"{topicName}[{metadata?.Partition}]@{metadata?.Offset}",
                    metadata?.MessageId);
            }
        }
    }

    internal static void LogErrorInRetryHandler(this ILogger logger,
        string topicName,
        KafkaMessageValueMetadata? metadata,
        Exception exception,
        int retryCount,
        MoedeloKafkaMessageValueBase message)
    {
        if (logger.IsEnabled(LogLevel.Error))
        {
            using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", message)))
            {
                var offset = $"{topicName}[{metadata?.Partition}]@{metadata?.Offset}";
                logger.LogErrorInRetryHandler(offset, exception, retryCount);
            }
        }
    }
    
    internal static void LogErrorButRetryScheduled(this ILogger logger,
        string topicName,
        KafkaMessageValueMetadata? metadata,
        Exception exception,
        TimeSpan timeout,
        int retryCount,
        MoedeloKafkaMessageValueBase message)
    {
        if (logger.IsEnabled(LogLevel.Error))
        {
            using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", message)))
            {
                var offset = $"{topicName}[{metadata?.Partition}]@{metadata?.Offset}";
                logger.LogErrorButRetryScheduled(offset, exception, timeout, retryCount);
            }
        }
    }
}
