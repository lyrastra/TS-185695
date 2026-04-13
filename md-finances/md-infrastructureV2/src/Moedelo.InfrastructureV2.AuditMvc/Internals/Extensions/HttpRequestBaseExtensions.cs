using System.Collections.Specialized;
using System.Web;
using Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.AuditMvc.Internals.Extensions;

internal static class HttpRequestBaseExtensions
{
    internal static IAuditSpanContext ExtractAuditTrailSpanContext(this HttpRequestBase httpContext)
    {
        var parentContext = GetParentContextFromHeaders(httpContext);

        return parentContext.IsEmpty() ? null : parentContext;
    }

    private static IAuditSpanContext GetParentContextFromHeaders(HttpRequestBase request)
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

    private static string GetParentScopeContextJson(NameValueCollection headers)
    {
        var parentScopeContextHeaders = headers.GetValues(AuditHeaderParam.ParentScopeContext);
        var parentScopeContext = (parentScopeContextHeaders?.Length ?? 0) > 0 ? parentScopeContextHeaders[0] : null;

        return parentScopeContext;
    }
}
