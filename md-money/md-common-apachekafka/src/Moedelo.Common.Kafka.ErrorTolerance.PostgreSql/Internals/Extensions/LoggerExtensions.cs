using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.DbModels;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(EventId = 11000, Level = LogLevel.Debug, SkipEnabledCheck = true,
        Message = "{topic}[{partition}]@{offset}: загружено состояние (глубина {depth}, попытка №{attemptNumber}, время: {loadingDuration:g}). Глоб.номер: {loadingGlobalCount}")]
    private static partial void LogStateIsLoaded(
        this ILogger logger,
        string topic,
        int partition,
        long? offset,
        int depth,
        int attemptNumber,
        TimeSpan loadingDuration,
        int loadingGlobalCount);

    [LoggerMessage(EventId = 11001, Level = LogLevel.Warning,
        Message = "{topic}[{partition}]@{offset}: загружено состояние (глубина {depth}, попытка №{attemptNumber}, время: {loadingDuration:g}). Глоб.номер: {loadingGlobalCount}")]
    private static partial void LogStateIsLoadedButTooSlow(
        this ILogger logger,
        string topic,
        int partition,
        long? offset,
        int depth,
        int attemptNumber,
        TimeSpan loadingDuration,
        int loadingGlobalCount);

    
    [LoggerMessage(EventId = 11002, Level = LogLevel.Debug, SkipEnabledCheck = true,
        Message = "{topic}[{partition}]@{offset}: сохраняется состояние (глубина {depth})")]
    private static partial void LogStateIsSaving(this ILogger logger, string topic, int partition, long? offset, int depth);
    
    [LoggerMessage(EventId = 11003, Level = LogLevel.Error, SkipEnabledCheck = true,
        Message = "{topic}[{partition}] ошибка при попытке загрузки состояния для {consumerGroupId}. Продолжительность: {loadingDuration:g}. Осталось повторных попыток: {leftAttempts}")]
    internal static partial void LogStateLoadingFailedError(this ILogger logger,
        Exception exception,
        string consumerGroupId,
        string topic,
        int partition,
        int leftAttempts,
        TimeSpan loadingDuration);

    internal static void LogStateIsSaving(this ILogger logger, PartitionConsumingDbModel dbModel)
    {
        if (logger.IsEnabled(LogLevel.Debug))
        {
            using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", dbModel)))
            {
                logger.LogStateIsSaving(dbModel.Topic, dbModel.Partition, dbModel.CommittedOffset, dbModel.OffsetMapDepth);
            }
        }
    }

    internal static void LogStateIsLoaded(
        this ILogger logger,
        IPartitionConsumingReadOnlyState dbModel,
        int attemptNumber,
        TimeSpan loadingDuration,
        int loadingGlobalCount)
    {
        if (logger.IsEnabled(LogLevel.Debug))
        {
            using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", dbModel)))
            {
                logger.LogStateIsLoaded(
                    dbModel.Topic,
                    dbModel.Partition,
                    dbModel.CommittedOffset,
                    dbModel.OffsetMapDepth,
                    attemptNumber,
                    loadingDuration,
                    loadingGlobalCount);
            }
        }
    }
    
    internal static void LogStateIsLoadedButTooSlow(
        this ILogger logger,
        IPartitionConsumingReadOnlyState dbModel,
        int attemptNumber,
        TimeSpan loadingDuration,
        int loadingGlobalCount)
    {
        using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", dbModel)))
        {
            logger.LogStateIsLoadedButTooSlow(
                dbModel.Topic,
                dbModel.Partition,
                dbModel.CommittedOffset,
                dbModel.OffsetMapDepth,
                attemptNumber,
                loadingDuration,
                loadingGlobalCount);
        }
    }
}
