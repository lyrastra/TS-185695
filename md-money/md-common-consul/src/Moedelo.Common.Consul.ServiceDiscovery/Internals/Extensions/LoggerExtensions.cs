using Microsoft.Extensions.Logging;

namespace Moedelo.Common.Consul.ServiceDiscovery.Internals.Extensions;

internal static partial class LoggerExtensions
{
    internal static void LogRegistrationError(this ILogger logger, Exception exception, string? serviceRegistrationId) =>
        logger.LogServiceError(exception, serviceRegistrationId, "Ошибка регистрации службы в Consul");

    internal static void LogFirstRegistration(this ILogger logger, LogLevel level, string? serviceRegistrationId) =>
        logger.LogServiceMessage(level, serviceRegistrationId,
            "Служба зарегистрирована в Consul (начальная регистрация)");

    internal static void LogRepeatedRegistration(this ILogger logger, LogLevel level, string? serviceRegistrationId) =>
        logger.LogServiceMessage(level, serviceRegistrationId,
        "Служба зарегистрирована в Consul (повторная регистрация)");

    [LoggerMessage(EventId = 3004, Level = LogLevel.Debug,
        Message = "{ServiceRegistrationId} отправка сообщения о снятии службы с регистрации...")]
    internal static partial void LogSendingRegistration(this ILogger logger, string serviceRegistrationId);

    internal static void LogDeregistrationDone(this ILogger logger, LogLevel level, string? serviceRegistrationId) =>
        logger.LogServiceMessage(level, serviceRegistrationId,
            "Служба снята с регистрации в Consul");

    internal static void LogDeregistrationError(this ILogger logger, Exception exception, string? serviceRegistrationId) =>
        logger.LogServiceError(exception, serviceRegistrationId, "Ошибка снятия службы с регистрации в Consul");

    internal static void LogRegistrationIsGoneError(this ILogger logger, string? serviceRegistrationId) =>
        logger.LogServiceMessage(LogLevel.Error, serviceRegistrationId,
            "Обнаружена пропажа регистрации службы в Consul Service Discovery. Будет выполнена попытка восстановления регистрации");

    [LoggerMessage(EventId = 3008, Level = LogLevel.Debug,
        Message = "{ServiceRegistrationId} функционирует в штатном режиме (отправлено {SuccessSentCount} уведомлений в Consul Service Discovery)")]
    internal static partial void LogServiceHealthStatusGood(this ILogger logger, string serviceRegistrationId, int successSentCount);

    internal static void LogHealthCheckSendingError(this ILogger logger, Exception exception, string serviceRegistrationId) =>
        logger.LogServiceError(exception, serviceRegistrationId, "Ошибка отправки уведомления о штатном функционировании в Consul Service Discovery");

    internal static void LogHealthCheckLoopInterruptedError(this ILogger logger, Exception exception, string serviceRegistrationId) =>
        logger.LogServiceError(exception, serviceRegistrationId, "цикл отправки уведомлений о штатном функционировании прерван исключением");

    [LoggerMessage(EventId = 3011, Level = LogLevel.Debug,
        Message = "{ServiceRegistrationId}: цикл отправки уведомлений о штатном функционировании остановлен")]
    internal static partial void LogHealthCheckLoopEnded(this ILogger logger, string serviceRegistrationId);
    
    private static void LogServiceMessage(this ILogger logger, LogLevel level, string? serviceRegistrationId, string message)
    {
        if (logger.IsEnabled(level) == false)
        {
            return;
        }

        using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", new { ServiceRegistrationId = serviceRegistrationId })))
        {
            logger.Log(level, message);
        }
    }

    private static void LogServiceError(this ILogger logger, Exception exception, string? serviceRegistrationId, string message)
    {
        using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", new { ServiceRegistrationId = serviceRegistrationId })))
        {
            logger.LogError(exception, message);
        }
    }
}
