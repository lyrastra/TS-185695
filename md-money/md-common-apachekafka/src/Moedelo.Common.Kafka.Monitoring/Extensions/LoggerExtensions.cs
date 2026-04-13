using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Monitoring.Models;

namespace Moedelo.Common.Kafka.Monitoring.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(EventId = 75001, Message = "Мониторинг кафки: сохранение в consul ключа {keyPath}")]
    internal static partial void LogSaveKey(this ILogger logger, LogLevel level, string keyPath);

    [LoggerMessage(EventId = 75002, Message = "Мониторинг кафки: отмена сохранения в consul ключа {keyPath}")]
    internal static partial void LogSaveKeyCancelled(this ILogger logger, LogLevel level, string keyPath);

    [LoggerMessage(EventId = 75003, Message = "Мониторинг кафки: удаление из consul ключа {keyPath}")]
    internal static partial void LogDeleteKey(this ILogger logger, LogLevel level, string keyPath);

    [LoggerMessage(EventId = 75004, Message = "Мониторинг кафки: работа Consul API останавливается")]
    internal static partial void LogKafkaConsulStops(this ILogger logger, LogLevel level);

    [LoggerMessage(EventId = 75005, Level = LogLevel.Error, 
        Message = "Мониторинг кафки: ошибка вызова consul API. Тип {callRequestType}, ключ {keyPath}")]
    internal static partial void LogConsulApiCallFailed(this ILogger logger,
        ConsulApiCallRequest.RequestType callRequestType, string keyPath, Exception exception);
}
