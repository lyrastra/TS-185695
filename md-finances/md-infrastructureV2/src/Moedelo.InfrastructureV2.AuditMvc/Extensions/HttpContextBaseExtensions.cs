#nullable enable
using System.Collections.Generic;
using System.Web;
using Moedelo.InfrastructureV2.AuditMvc.Internals;

namespace Moedelo.InfrastructureV2.AuditMvc.Extensions;

public static class HttpContextBaseExtensions
{
    internal static void PushAuditTrailScope(this HttpContextBase httpContext, MvcAuditTrailScope scope)
    {
        var stack = httpContext.Items[HttpContextItemName.ActionScopesStack] as Stack<MvcAuditTrailScope>
                ?? new Stack<MvcAuditTrailScope>();

        stack.Push(scope);
        httpContext.Items[HttpContextItemName.ActionScopesStack] = stack;
    }

    internal static MvcAuditTrailScope PopAuditTrailScope(this HttpContextBase httpContext)
    {
        var stack = httpContext.Items[HttpContextItemName.ActionScopesStack] as Stack<MvcAuditTrailScope>;

        return stack?.Count > 0 ? stack.Pop() : MvcAuditTrailScope.Null;
    }

    internal static MvcAuditTrailScope PeekAuditTrailScope(this HttpContextBase httpContext)
    {
        var stack = httpContext.Items[HttpContextItemName.ActionScopesStack] as Stack<MvcAuditTrailScope>;

        return stack?.Count > 0 ? stack.Peek() : MvcAuditTrailScope.Null;
    }

    /// <summary>
    /// Возвращает ip пользователя. Если не удалось определить, возвращает 127.0.0.1.
    /// </summary>
    public static string GetClientIp(this HttpContextBase httpContext)
    {
        const string localhostIp = "127.0.0.1";

        var httpXRealIp = httpContext.Request.ServerVariables["HTTP_X_REAL_IP"]; 
        var httpXForwardedFor = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        var remoteAddr = httpContext.Request.ServerVariables["REMOTE_ADDR"];

        var ip = string.IsNullOrEmpty(httpXRealIp)
            ? (string.IsNullOrEmpty(httpXForwardedFor) ? remoteAddr : httpXForwardedFor)
            : httpXRealIp;

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
}
