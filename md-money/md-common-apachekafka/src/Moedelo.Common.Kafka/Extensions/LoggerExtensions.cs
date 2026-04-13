using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Extensions;

internal static class LoggerExtensions
{
    internal static void LogRebalanceStart(this ILogger logger,
        LogLevel logLevel,
        IConsumerObserverCollection consumers,
        KafkaRebalanceRequirements requirements)
    {
        if (!logger.IsEnabled(logLevel))
            return;

        logger.Log(
            logLevel,
            "{TopicName}: начата процедура ребалансировки. Квота: {Quota}. Запущено: {ConsumersCount}",
            consumers.Settings.TopicName.Raw,
            requirements.Quota,
            consumers.Count);
    }

    internal static void LogRebalanceFinish(this ILogger logger,
        LogLevel logLevel,
        IConsumerObserverCollection consumers,
        KafkaRebalanceRequirements requirements)
    {
        if (!logger.IsEnabled(logLevel))
            return;

        logger.Log(
            logLevel,
            "{TopicName}: закончена процедура ребалансировки. Квота: {Quota}. Запущено: {ConsumersCount}",
            consumers.Settings.TopicName.Raw,
            requirements.Quota,
            consumers.Count);
    }

    internal static void LogReadyToBalance(this ILogger logger, LogLevel logLevel,
        IConsumerObserverCollection consumers)
    {
        if (!logger.IsEnabled(logLevel))
            return;

        logger.Log(
            logLevel, "{TopicName}: подключена автобалансировка консьюмеров группы {GroupId}",
            consumers.Settings.TopicName.Raw,
            consumers.Settings.GroupId.Raw);
    }

    internal static void LogBalanceRegression(this ILogger logger, LogLevel logLevel,
        KafkaConsumerConfig kafkaConsumerSettings)
    {
        if (!logger.IsEnabled(logLevel))
            return;

        logger.Log(
            logLevel, "{TopicName}: автобалансировка отключена для данного окружения. Будет запущен один консьюмер",
            kafkaConsumerSettings.TopicName.Raw);
    }

    internal static void LogConsumerHasBeingStarted(
        this ILogger logger,
        IConsumerObserverCollection consumers)
    {
        if (!logger.IsEnabled(LogLevel.Debug))
            return;

        logger.LogDebug("{TopicName}: запущен новый консьюмер. Всего: {ConsumersCount}",
            consumers.Settings.TopicName.Raw,
            consumers.Count);
    }

    internal static void LogConsumerHasBeingStopped(
        this ILogger logger,
        IConsumerObserverCollection consumers)
    {
        if (!logger.IsEnabled(LogLevel.Debug))
            return;

        logger.LogDebug("{TopicName}: консьюмер успешно остановлен",
            consumers.Settings.TopicName.Raw);
    }

    internal static void LogConsumerStoppingError(
        this ILogger logger,
        IConsumerObserverCollection consumers)
    {
        logger.LogError(
            "{TopicName}: не удалось дождаться полной остановки консьюмера",
            consumers.Settings.TopicName.Raw);
    }

    internal static void LogErrorIfStopMiscountPresents(
        this ILogger logger,
        IConsumerObserverCollection consumers,
        int stopCountRequirement,
        int reallyStoppedCount)
    {
        if (reallyStoppedCount != stopCountRequirement)
        {
            logger.LogError(
                "{TopicName}: остановлено только {ReallyStoppedCount} консьюмеров при заявке на остановку {StopCountRequirement}. Всего консьюмеров: {ConsumersCount}",
                consumers.Settings.TopicName.Raw,
                reallyStoppedCount,
                stopCountRequirement,
                consumers.Count);
        }
    }
}
