using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Moedelo.Common.AspNet.HostedServices.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(LogLevel.Information, "{serviceName} стал лидером")]
    private static partial void LogBecameMaster(this ILogger logger, string serviceName);

    internal static void LogBecameMaster(this ILogger logger, string serviceName, string sessionId)
    {
        using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", new { SessionId = sessionId })))
        {
            logger.LogBecameMaster(serviceName);
        }
    }

    [LoggerMessage(LogLevel.Information, "{serviceName} не стал лидером")]
    private static partial void LogNotMaster(this ILogger logger, string serviceName);

    internal static void LogNotMaster(this ILogger logger, string serviceName, string sessionId)
    {
        using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", new { SessionId = sessionId })))
        {
            logger.LogNotMaster(serviceName);
        }
    }

    [LoggerMessage(LogLevel.Debug, "Попытка захвата лидерства для {serviceName} с ключом {leadershipLockId}")]
    internal static partial void LogLeadershipAcquiring(this ILogger logger, string serviceName, string leadershipLockId);

    [LoggerMessage(LogLevel.Debug, "Результат захвата лидерства для {serviceName}: {result}", SkipEnabledCheck = true)]
    private static partial void LogLeadershipAcquiringResult(this ILogger logger, string serviceName, bool? result);

    internal static void LogLeadershipAcquiringResult(this ILogger logger, string serviceName, bool? result, string sessionId)
    {
        if (!logger.IsEnabled(LogLevel.Debug))
        {
            return;
        }

        using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", new { SessionId = sessionId })))
        {
            logger.LogLeadershipAcquiringResult(serviceName, result);
        }
    }

    [LoggerMessage(LogLevel.Error, "Ошибка при попытке захвата лидерства для {serviceName} с ключом {leadershipLockId}")]
    internal static partial void LogLeadershipAcquiringError(this ILogger logger, Exception exception, string serviceName, string leadershipLockId);

    [LoggerMessage(LogLevel.Debug, "Освобождение лидерства для {serviceName} с ключом {leadershipLockId}")]
    internal static partial void LogLeadershipReleasing(this ILogger logger, string serviceName, string leadershipLockId);

    [LoggerMessage(LogLevel.Error, "Ошибка при освобождении лидерства для {serviceName} с ключом {leadershipLockId}")]
    internal static partial void LogLeadershipReleasingError(this ILogger logger, Exception exception, string serviceName, string leadershipLockId);
}
