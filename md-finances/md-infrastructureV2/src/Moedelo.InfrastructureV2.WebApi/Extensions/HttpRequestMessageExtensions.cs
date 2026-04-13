#nullable enable
using System.Net.Http;
using System.Web;

namespace Moedelo.InfrastructureV2.WebApi.Extensions;

public static class HttpRequestMessageExtensions
{
    public static string? GetClientIp(this HttpRequestMessage request)
    {
        const string localhostIp = "127.0.0.1";

        var httpContext = request.GetHttContext();
        
        if (httpContext == null)
        {
            return null;
        }

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

    public static HttpContextBase? GetHttContext(this HttpRequestMessage requestMessage)
    {
        const string msHttpContextPropName = "MS_HttpContext";
        
        return requestMessage.Properties.TryGetValue(msHttpContextPropName, out var value)
            ? value as HttpContextWrapper
            : null;
    }
}
