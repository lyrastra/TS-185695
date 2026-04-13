#nullable enable
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;

namespace Moedelo.InfrastructureV2.AuditWebApi.Extensions;

internal static class HttpRequestMessageExtensions
{
    private static readonly Regex TemplateArgsRegex = new Regex(@"{.*?}",
        RegexOptions.Compiled,
        matchTimeout: TimeSpan.FromSeconds(1));
    
    /// <summary>
    /// Получить имя спана из шаблона маршрута, использованного для исполнения запроса
    /// ВНИМАНИЕ: для шаблонов путей, отличающихся только ограничениями на аргумент, значение здесь будет одинаковым,
    /// например, [Route('foo/{id:int}/bar')] и [Route(''foo/{id:string}/bar')] додут одинаковое значение 'foo/{id}/bar'.
    /// Я пока не нашёл способа их различить.
    /// В итоге такие два метода "сольются" в один в auditTrail.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>нормализованное название спана на основании шаблона маршрута запроса (м.б. null)</returns>
    internal static string? GetAuditSpanNameFromRouteTemplate(this HttpRequestMessage request)
    {
        var httpRouteData = request.GetRouteData();
        var route = httpRouteData.Route;
        var routeTemplate = route.RouteTemplate;

        if (string.IsNullOrEmpty(routeTemplate))
        {
            return null;
        }
        
        if (routeTemplate.IndexOf("{controller}", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            // похоже, это какой-то стандартный шаблон маршрута, определяющий общие правила маршрутизации
            // это значение будет одинаковым для всех методов, для которых оно использовано для маршрутизации,
            // т.е. не является уникальным и не подходит в качестве уникального идентификатора. 
            // лучше оставим название спана, полученное из uri запроса 
            return null;
        }

         
        var method = request.Method.Method;
        var path = TemplateArgsRegex.Replace(routeTemplate.ToLowerInvariant(), "...");

        return $"{method} ./{path}";
    }
    
    private static HttpContextBase? GetHttContext(this HttpRequestMessage requestMessage)
    {
        const string msHttpContextPropName = "MS_HttpContext";
        
        return requestMessage.Properties.TryGetValue(msHttpContextPropName, out var property)
            ? property as HttpContextWrapper
            : null;
    }

    internal static string? GetUserAgent(this HttpRequestMessage request)
    {
        return request.GetHttContext()?.Request.UserAgent;
    }

    internal static string? GetClientIp(this HttpRequestMessage request)
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
}
