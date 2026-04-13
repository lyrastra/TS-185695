using System.Web.Mvc;
using System.Web.Routing;
using Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;

namespace Moedelo.InfrastructureV2.AuditMvc.Internals.Extensions;

internal static class ActionExecutingContextExtensions
{
    private const string ReplacementPattern = "...";

    internal static SpanName GetAuditSpanName(this ActionExecutingContext actionContext)
    {
        var routeData = actionContext.HttpContext.Request.RequestContext.RouteData;
        var request = actionContext.HttpContext.Request;

        if (routeData?.Route is Route)
        {
            var urlHelper = new UrlHelper(actionContext.RequestContext);
            var values = new RouteValueDictionary(routeData.Values);
            foreach (var keyValue in actionContext.ActionParameters)
            {
                if (values.ContainsKey(keyValue.Key))
                {
                    values[keyValue.Key] = ReplacementPattern;
                }
            }

            values["id"] = ReplacementPattern;

            var routeUrl = urlHelper.RouteUrl(values);

            if (routeUrl != null)
            {
                return new SpanName($"{request.HttpMethod} .{routeUrl.ToLower()}", isNormalized: true);
            }
        }

        return new SpanName(request.Url?.GetAuditSpanName(request.HttpMethod), isNormalized: false);
    }
}
