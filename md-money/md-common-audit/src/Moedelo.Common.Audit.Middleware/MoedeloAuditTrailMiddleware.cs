using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moedelo.Common.Audit.Abstractions.Helpers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.Audit.Middleware.Extensions;
using Moedelo.Common.Audit.Middleware.Internals;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Audit.Middleware;

internal sealed class MoedeloAuditTrailMiddleware
{
    private readonly RequestDelegate next;
    private readonly Regex[] excludeRequestBodyUrlRegexes;
    private readonly Regex[] excludeResponseBodyUrlRegexes;
    private readonly Regex[] excludeUrlRegexes;
    private readonly bool treatValidationExceptionAsError;
    /// <summary>
    /// Добавить IP-адрес клиента
    /// </summary>
    private readonly bool tagClientIpAddress;

    public MoedeloAuditTrailMiddleware(RequestDelegate next,
        AuditApiHandlerTraceMiddlewareSettings settings)
    {
        this.next = next;

        excludeRequestBodyUrlRegexes = (settings?.ExcludeRequestBodyUrlPatterns ?? ArraySegment<string>.Empty)
            .Select(pattern => new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase))
            .ToArray();
        excludeResponseBodyUrlRegexes = (settings?.ExcludeResponseBodyUrlPatterns ?? ArraySegment<string>.Empty)
            .Select(pattern => new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase))
            .ToArray();
        excludeUrlRegexes = (settings?.ExcludeUrlPatterns ?? ArraySegment<string>.Empty)
            .Select(pattern => new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase))
            .ToArray();
        treatValidationExceptionAsError = settings?.TreatValidationExceptionAsError == true;
        tagClientIpAddress = settings?.TagClientIpAddress == true;
    }

    public Task Invoke(HttpContext httpContext, IAuditTracer auditTracer)
    {
        if (excludeUrlRegexes.IsAnyMatched(httpContext.Request.Path))
        {
            return next(httpContext);
        }

        return InvokeInsideAuditTrailScope(httpContext, auditTracer);
    }

    private async Task InvokeInsideAuditTrailScope(HttpContext httpContext, IAuditTracer auditTracer)
    {
        using var scope = auditTracer
            .BuildSpan(AuditSpanType.ApiHandler, httpContext.Request.GetAuditSpanName())
            .AsChildOf(GetParentContext(httpContext))
            .Start();

        if (scope.IsEnabled == false)
        {
            // запуск с отключенным audit trail
            await next(httpContext);
            return;
        }

        var includeRequestBody = NeedIncludeRequestBody(httpContext);
        var includeResponseBody = NeedIncludeResponseBody(httpContext);

        var auditTrailSpan = scope.Span;
        // сохраняем данные, необходимые для корректной работы фильтра, сохраняющего ответ в auditTrail
        httpContext.Items[HttpContextItemNames.MdAuditTrailSpanName] = auditTrailSpan;
        httpContext.Items[HttpContextItemNames.MdAuditTrailDumpResponseBodyMode] = includeResponseBody;

        await auditTrailSpan.TryAddRequestAsync(httpContext.Request, includeRequestBody, tagClientIpAddress);

        try
        {
            await next(httpContext);
            auditTrailSpan.TagResponseStatusCode(httpContext, treatValidationExceptionAsError);
        }
        catch (Exception exception) when (treatValidationExceptionAsError || exception is not ValidationException)
        {
            auditTrailSpan.SetError(exception);

            throw;
        }
        finally
        {
            httpContext.Items.Remove(HttpContextItemNames.MdAuditTrailDumpResponseBodyMode);
            httpContext.Items.Remove(HttpContextItemNames.MdAuditTrailSpanName);
        }
    }

    private bool NeedIncludeResponseBody(HttpContext httpContext)
    {
        return excludeResponseBodyUrlRegexes.IsAnyMatched(httpContext.Request.Path) == false;
    }

    private bool NeedIncludeRequestBody(HttpContext httpContext)
    {
        return excludeRequestBodyUrlRegexes.IsAnyMatched(httpContext.Request.Path) == false;
    }

    private static IAuditSpanContext GetParentContext(HttpContext httpContext)
    {
        try
        {
            var parentScopeContextJson = GetParentScopeContextJson(httpContext.Request.Headers);
            var auditTrailContext = string.IsNullOrWhiteSpace(parentScopeContextJson)
                ? null
                : parentScopeContextJson.FromJsonString<ContextFromRequest>();

            if (auditTrailContext.IsNullOrEmpty())
            {
                return null;
            }

            return auditTrailContext;
        }
        catch
        {
            return null;
        }
    }

    private static string GetParentScopeContextJson(IHeaderDictionary headers)
    {
        var requestIdHeaders = headers[AuditHeaderParamHelper.ParentScopeContext];

        return requestIdHeaders.Count == 1
            ? requestIdHeaders[0]
            : null;
    }
}
