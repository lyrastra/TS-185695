#nullable enable
using Microsoft.AspNetCore.Http;

namespace Moedelo.Common.Audit.Middleware.Extensions;

internal static class HttpContextExtensions
{
    private const string LocalhostIp = "127.0.0.1";

    /// <summary>
    /// Возвращает ip пользователя. Если не удалось определить, возвращаем 127.0.0.1.
    /// Работает для stage|prod-окружений moedelo. В прочих окружениях результат не определён
    /// </summary>
    internal static string? GetClientIp(this HttpContext? httpContext, bool allowNull)
    {
        if (httpContext == null)
        {
            return allowNull ? null : LocalhostIp;
        }

        var ip = httpContext.Request.Headers.GetFirstValueOrDefault("X-Real-IP")
            ?? httpContext.Request.Headers.GetFirstValueOrDefault("X-Forwarded-For")
            ?? httpContext.Connection.RemoteIpAddress?.ToString();

        if (ip == "::1")
        {
            return LocalhostIp;
        }

        return ip ?? (allowNull ? null : LocalhostIp);
    }

    private static string? GetFirstValueOrDefault(this IHeaderDictionary headers, string name)
    {
        if (headers.TryGetValue(name, out var values) && values.Count > 0)
        {
            return values[0];
        }

        return default;
    }
}
