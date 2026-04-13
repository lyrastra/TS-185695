#nullable enable
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Moedelo.Common.Audit.Middleware.Extensions;

internal static class ActionExecutedContextExtensions
{
    internal static string? GetAuditTrailSpanName(this ActionExecutedContext actionContext)
    {
        var routeTemplate = actionContext.ActionDescriptor.AttributeRouteInfo?.Template;
        
        if (routeTemplate == null)
        {
            // unable detect
            return null;
        }

        var routeValues = actionContext.RouteData.Values;

        if (routeValues.TryGetValue("version", out var version))
        {
            routeTemplate = Regex.Replace(
                routeTemplate,
                @"\{version:apiVersion\}", $"{version}",
                RegexOptions.Compiled | RegexOptions.CultureInvariant);
        }
        
        routeTemplate = Regex.Replace(
            routeTemplate,
            @"\{(\w+)(:\w+)?\}", "...", RegexOptions.Compiled
        );
        
        var httpMethod = actionContext.HttpContext.Request.Method;
        var baseUrl = actionContext.HttpContext.Request.PathBase.ToString();

        return $"{httpMethod} .{baseUrl.ToLowerInvariant()}/{routeTemplate.ToLowerInvariant()}";
    }
}
