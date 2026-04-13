#nullable enable
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Moedelo.Common.ExecutionContext.Middleware.Extensions;

internal static partial class LoggerExtensions
{
    internal static void LogTokenAuthorizationFailed(this ILogger logger, Exception exception, string? token)
    {
        var tokenLength = token?.Length ?? 0;
        var tokenDumpLength = Math.Min(512, tokenLength);
        var tokenDump = token?[..tokenDumpLength];
        using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", new { tokenDump, tokenDumpLength, tokenLength })))
        {
            logger.LogError(exception, "Ошибка при попытке авторизовать токен");
        }
    }

    internal static void LogErrorInExecutionInfoContextInvoke(this ILogger logger, Exception exception)
    {
        logger.LogError(exception,
            "Ошибка при выполнении {MiddlewareName}.InvokeAsync",
            nameof(ExecutionInfoContextMiddleware));
    }
}
