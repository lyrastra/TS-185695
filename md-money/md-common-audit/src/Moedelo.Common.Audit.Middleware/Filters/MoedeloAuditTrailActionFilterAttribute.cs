#nullable enable
using System.Collections.Concurrent;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Middleware.Extensions;
using Moedelo.Common.Audit.Middleware.Internals;

namespace Moedelo.Common.Audit.Middleware.Filters;

/// <summary>
/// - Меняет имя спана на нормализованное
/// - Добавляет в ответ заголовок <see cref="CustomHeaderNames.AuditTrailContext"/>
/// - Делает дамп ответа в auditTrail
/// </summary>
internal sealed class MoedeloAuditTrailActionFilterAttribute : ActionFilterAttribute
{
    private readonly ConcurrentDictionary<string, string?> spanNameByActionIdMap = new();

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (GetMdAuditTrailSpanRef(context) is { } auditTrailSpan)
        {
            CorrectAuditTrailSpanName(context, auditTrailSpan);
            AddAuditTrailResponseHeader(context, auditTrailSpan);
            DumpResponseToAuditTrailSpan(context, auditTrailSpan);
        }

        base.OnActionExecuted(context);
    }

    private static void AddAuditTrailResponseHeader(ActionContext context, IAuditSpan auditTrailSpan)
    {
        context.HttpContext.Response.AddAuditTrailHeaders(auditTrailSpan);
    }

    private static void DumpResponseToAuditTrailSpan(ActionExecutedContext context, IAuditSpan auditTrailSpan)
    {
        if (IsResponseBodyDumpAllowed(context))
        {
            if (context.Result is ObjectResult objectResult)
            {
                auditTrailSpan.AddResponseBody(objectResult);
            }
        }
    }

    private void CorrectAuditTrailSpanName(ActionExecutedContext context, IAuditSpan auditTrailSpan)
    {
        var spanName = GetSpanName(context);

        if (spanName != null)
        {
            auditTrailSpan.SetName(spanName);
        }
    }

    private string? GetSpanName(ActionExecutedContext context)
    {
        var actionId = context.ActionDescriptor.Id;

        if (spanNameByActionIdMap.TryGetValue(actionId, out var spanName))
        {
            return spanName;
        }

        spanName = context.GetAuditTrailSpanName();

        spanNameByActionIdMap.TryAdd(actionId, spanName);

        return spanName;
    }

    private static bool IsResponseBodyDumpAllowed(ActionExecutedContext context)
    {
        if (context.Exception is not null)
        {
            // если обработка завершилась с ошибкой, ничего сохранять не надо
            return false;
        }

        if (context.HttpContext.Response.ContentType != MediaTypeNames.Application.Json)
        {
            // если не json не дампим
            return false;
        }

        return context.HttpContext.Items.TryGetValue(HttpContextItemNames.MdAuditTrailDumpResponseBodyMode,
            out var needDumpResponseBody) && needDumpResponseBody is true;
    }

    private static IAuditSpan? GetMdAuditTrailSpanRef(ActionContext context)
    {
        return context.HttpContext.Items.TryGetValue(HttpContextItemNames.MdAuditTrailSpanName, out var item)
            ? item as IAuditSpan
            : null;
    }
}
