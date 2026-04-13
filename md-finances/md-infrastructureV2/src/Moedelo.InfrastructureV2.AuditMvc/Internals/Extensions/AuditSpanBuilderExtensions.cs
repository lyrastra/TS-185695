using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.AuditMvc.Internals.Extensions;

internal static class AuditSpanBuilderExtensions
{
    internal static IAuditSpanBuilder WithRequestTags(this IAuditSpanBuilder spanBuilder,
        ActionExecutingContext actionContext, IReadOnlyCollection<Regex> excludeRequestBodyForUrls,
        bool addClientIpAddressTag, bool addUserAgent = false)
    {
        var includeRequestBody = excludeRequestBodyForUrls
            .Any(regex => regex.IsMatch(actionContext.HttpContext.Request.Path)) == false;

        actionContext.HttpContext.Request.EnumerateRequestTags(
            actionContext.ActionParameters,
            includeRequestBody, addClientIpAddressTag, addUserAgent,
            (tagName, tagValue) => spanBuilder.WithTag(tagName, tagValue));

        return spanBuilder;
    }
}
