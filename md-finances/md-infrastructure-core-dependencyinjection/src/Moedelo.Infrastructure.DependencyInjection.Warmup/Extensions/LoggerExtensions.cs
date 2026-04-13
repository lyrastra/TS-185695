using System;
using Microsoft.Extensions.Logging;

namespace Moedelo.Infrastructure.DependencyInjection.Warmup.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(EventId = 21, Level = LogLevel.Error,
        Message = "Application warmup: не удалось создать экземпляр типа {type}")]
    internal static partial void LogTypeInstantiationFailed(this ILogger logger, Exception exception, Type type);

    [LoggerMessage(EventId = 22, Level = LogLevel.Error,
        Message = "Приложение будет остановлено: стартовый прогрев приложения завершился с ошибкой")]
    internal static partial void LogWarmUpFailed(this ILogger logger, Exception exception);

    [LoggerMessage(EventId = 23, Level = LogLevel.Information,
        Message = "Стартовый прогрев приложения успешно завершён. Проверено типов сервисов: {Count}. Затраченное время: {Duration:g}")]
    internal static partial void LogWarmUpComplete(this ILogger logger, int count, TimeSpan duration);
    
    [LoggerMessage(EventId = 24, Level = LogLevel.Debug,
        Message = "Стартовый прогрев приложения начался. Сервисов на проверку: {servicesCount}, степень параллелизма: {degreeOfParallelism}")]
    internal static partial void LogWarmUpStarted(this ILogger logger, int servicesCount, int degreeOfParallelism);
}
