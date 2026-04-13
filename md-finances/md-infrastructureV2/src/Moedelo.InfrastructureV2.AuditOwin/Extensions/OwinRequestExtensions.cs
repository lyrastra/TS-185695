using Microsoft.Owin;
using Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.AuditOwin.Extensions;

/// <summary>
/// Расширения для работы с контекстом аудита в OWIN
/// </summary>
public static class OwinRequestExtensions
{
    /// <summary>
    /// Получает имя span
    /// </summary>
    public static string GetAuditTrailSpanName(this IOwinRequest request)
    {
        var path = request.Path.Value?.ToLowerInvariant() ?? "";

        return $"{request.Method} .{path}";
    }

    /// <summary>
    /// Извлекает родительский контекст аудита из заголовков OWIN запроса
    /// </summary>
    /// <param name="request">OWIN запрос</param>
    /// <returns>Родительский контекст аудита или null, если не найден</returns>
    public static IAuditSpanContext ExtractAuditTrailSpanContext(this IOwinRequest request)
    {
        var parentContext = GetParentContextFromHeaders(request);

        return parentContext?.IsEmpty() == false ? parentContext : null;
    }

    /// <summary>
    /// Получает IP адрес клиента из OWIN контекста
    /// </summary>
    public static string GetClientIpAddress(this IOwinRequest request)
    {
        const string localhostIp = "127.0.0.1";

        // Получаем IP из заголовков (аналог ServerVariables в OWIN)
        var httpXRealIp = request.Headers["X-Real-IP"];
        var httpXForwardedFor = request.Headers["X-Forwarded-For"];
        var remoteAddr = request.RemoteIpAddress;

        var ip = string.IsNullOrEmpty(httpXRealIp)
            ? (string.IsNullOrEmpty(httpXForwardedFor) ? remoteAddr : httpXForwardedFor)
            : httpXRealIp;

        if (string.IsNullOrEmpty(ip))
        {
            return null;
        }

        var commaIndex = ip.IndexOf(',');

        if (commaIndex > 0)
        {
            ip = ip.Substring(0, commaIndex).Trim();
        }

        if (ip == "::1")
        {
            return localhostIp;
        }

        return ip;
    }

    /// <summary>
    /// Получает родительский контекст из заголовков OWIN запроса
    /// </summary>
    /// <param name="request">OWIN запрос</param>
    /// <returns>Родительский контекст или null</returns>
    private static IAuditSpanContext GetParentContextFromHeaders(IOwinRequest request)
    {
        try
        {
            var parentScopeContextJson = GetParentScopeContextJson(request.Headers);
            return string.IsNullOrWhiteSpace(parentScopeContextJson)
                ? null
                : parentScopeContextJson.FromJsonString<ContextFromRequest>();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Получает JSON строку родительского контекста из заголовков
    /// </summary>
    /// <param name="headers">Заголовки OWIN запроса</param>
    /// <returns>JSON строка или null</returns>
    private static string GetParentScopeContextJson(IHeaderDictionary headers)
    {
        var hasHeader = headers.TryGetValue(AuditHeaderParam.ParentScopeContext, out var parentScopeContextValues);
        return hasHeader && parentScopeContextValues?.Length > 0 ? parentScopeContextValues[0] : null;
    }
}
