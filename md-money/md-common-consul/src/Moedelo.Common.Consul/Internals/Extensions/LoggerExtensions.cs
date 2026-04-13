using Microsoft.Extensions.Logging;

namespace Moedelo.Common.Consul.Internals.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(LogLevel.Debug, "Начинается создание сессии Consul: {SessionName}")]
    internal static partial void LogSessionCreationStarted(this ILogger logger, string sessionName);

    [LoggerMessage(LogLevel.Debug, "Сессия Consul успешно создана. SessionId: {SessionId}, Name: {SessionName}")]
    internal static partial void LogSessionCreated(this ILogger logger, string sessionId, string sessionName);

    [LoggerMessage(LogLevel.Error, "Ошибка создания сессии Consul. Name: {SessionName}")]
    internal static partial void LogSessionCreationFailed(this ILogger logger, Exception exception, string sessionName);

    [LoggerMessage(LogLevel.Debug, "Начинается валидация сессии Consul: {SessionId}")]
    internal static partial void LogSessionValidationStarted(this ILogger logger, string sessionId);

    [LoggerMessage(LogLevel.Debug, "Сессия Consul валидна: {SessionId}")]
    internal static partial void LogSessionValidated(this ILogger logger, string sessionId);

    [LoggerMessage(LogLevel.Error, "Ошибка валидации сессии Consul: {SessionId}")]
    internal static partial void LogSessionValidationFailed(this ILogger logger, Exception exception, string sessionId);

    [LoggerMessage(LogLevel.Trace, "Начинается обновление сессии Consul: {SessionId}")]
    internal static partial void LogSessionRenewalStarted(this ILogger logger, string sessionId);

    [LoggerMessage(LogLevel.Trace, "Сессия Consul обновлена: {SessionId}")]
    internal static partial void LogSessionRenewed(this ILogger logger, string sessionId);

    [LoggerMessage(LogLevel.Warning, "Ошибка обновления сессии Consul: {SessionId}")]
    internal static partial void LogSessionRenewalFailed(this ILogger logger, Exception exception, string sessionId);

    [LoggerMessage(LogLevel.Debug, "Попытка захвата ключа. SessionId: {SessionId}, KeyPath: {KeyPath}")]
    internal static partial void LogKeyAcquisitionStarted(this ILogger logger, string sessionId, string keyPath);

    [LoggerMessage(LogLevel.Debug, "Ключ успешно захвачен. SessionId: {SessionId}, KeyPath: {KeyPath}")]
    internal static partial void LogKeyAcquisitionCompleted(this ILogger logger, string sessionId, string keyPath);

    [LoggerMessage(LogLevel.Debug, "Ключ не удалось захватить (возможно, занят другим процессом). SessionId: {SessionId}, KeyPath: {KeyPath}")]
    internal static partial void LogKeyAcquisitionFailed(this ILogger logger, string sessionId, string keyPath);

    [LoggerMessage(LogLevel.Error, "Ошибка захвата ключа. SessionId: {SessionId}, KeyPath: {KeyPath}")]
    internal static partial void LogKeyAcquisitionError(this ILogger logger, Exception exception, string sessionId, string keyPath);

    [LoggerMessage(LogLevel.Debug, "Начинается освобождение ключа. SessionId: {SessionId}, KeyPath: {KeyPath}")]
    internal static partial void LogKeyReleaseStarted(this ILogger logger, string sessionId, string keyPath);

    [LoggerMessage(LogLevel.Debug, "Ключ успешно освобожден. SessionId: {SessionId}, KeyPath: {KeyPath}")]
    internal static partial void LogKeyReleaseCompleted(this ILogger logger, string sessionId, string keyPath);

    [LoggerMessage(LogLevel.Error, "Ошибка освобождения ключа. SessionId: {SessionId}, KeyPath: {KeyPath}")]
    internal static partial void LogKeyReleaseFailed(this ILogger logger, Exception exception, string sessionId, string keyPath);

    [LoggerMessage(LogLevel.Debug, "Сессия Consul освобождена: {SessionId}")]
    internal static partial void LogSessionDisposed(this ILogger logger, string sessionId);
} 