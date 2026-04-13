using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Moedelo.InfrastructureV2.AuditMvc.Extensions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Extensions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;

namespace Moedelo.InfrastructureV2.AuditMvc.Internals.Extensions;

internal static class AuditTracerExtensions
{
    internal static void StartRootActionAuditTrailSpan(this IAuditTracer auditTracer,
        ActionExecutingContext actionContext,
        Regex[] excludeRequestBodyForUrls, bool addClientIpAddressTag, bool addUserAgentTag)
    {
        if (actionContext.IsChildAction)
        {
            return;
        }

        if (!auditTracer.IsAuditTrailOn())
        {
            return;
        }

        var request = actionContext.HttpContext.Request;
        var parentContext = request.ExtractAuditTrailSpanContext();
        var spanName = actionContext.GetAuditSpanName();

        var scope = auditTracer
            .BuildSpan(AuditSpanType.ApiHandler, spanName.Name)
            .AsChildOf(parentContext)
            .WithRequestTags(actionContext, excludeRequestBodyForUrls, addClientIpAddressTag, addUserAgentTag)
            .Start();

        if (spanName.IsNormalized)
        {
            scope.Span.SetNormalizedName(spanName.Name);
        }

        actionContext.HttpContext.PushAuditTrailScope(MvcAuditTrailScope.ActionExecuting(scope));
    }

    internal static void StartChildChildAuditTrailSpan(
        this IAuditTracer auditTracer,
        ActionExecutingContext actionContext)
    {
        if (!actionContext.IsChildAction)
        {
            return;
        }

        if (!auditTracer.IsAuditTrailOn())
        {
            return;
        }

        var spanName = actionContext.ActionDescriptor.GetChildActionAuditTrailSpanName();

        var scope = auditTracer
            .BuildSpan(AuditSpanType.InternalCode, spanName)
            .Start();

        actionContext.HttpContext.PushAuditTrailScope(MvcAuditTrailScope.ActionExecuting(scope));
    }

    internal static void StartResultExecutingAuditTrailSpan(
        this IAuditTracer auditTracer,
        ResultExecutingContext resultContext)
    {
        if (!auditTracer.IsAuditTrailOn())
        {
            return;
        }

        var parentScope = resultContext.HttpContext.PeekAuditTrailScope();

        if (parentScope.Scope.IsEnabled == false)
        {
            return;
        }

        var spanName = parentScope.Scope.Span.Name.TrimEnd('/') + "/::ResultExecuting";

        var newScope = auditTracer
            .BuildSpan(AuditSpanType.InternalCode, spanName)
            .Start();

        if (parentScope.Scope.Span.IsNameNormalized)
        {
            newScope.Span.SetNormalizedName(spanName);
        }

        resultContext.HttpContext.PushAuditTrailScope(MvcAuditTrailScope.ResultExecuting(newScope));
    }
    
    internal static void CloseCurrentHttpContextAuditTrailScope(this HttpContextBase httpContext,
        Exception exception,
        bool isChildAction,
        bool treatValidationExceptionAsError)
    {
        var mvcScope = httpContext.PopAuditTrailScope();

        if (mvcScope.Scope.IsEnabled == false)
        {
            return;
        }

        if (!isChildAction && mvcScope.Type == MvcAuditTrailScopeType.ActionExecuting)
        {
            mvcScope.Scope.Span.TryAddResponse(httpContext.Response, treatValidationExceptionAsError);
        }

        if (exception != null)
        {
            if (treatValidationExceptionAsError || exception.GetType().Name != "ValidationFailureException")
            {
                mvcScope.Scope.Span.SetError(exception);
            }
        }

        // Добавляем заголовок аудита для корневых действий (как при успехе, так и при ошибках)
        if (!isChildAction && mvcScope.Type == MvcAuditTrailScopeType.ActionExecuting)
        {
            httpContext.Response.AddAuditTrailResponseHeader(mvcScope.Scope.Span);
        }

        mvcScope.Scope.Dispose();
    }

}
