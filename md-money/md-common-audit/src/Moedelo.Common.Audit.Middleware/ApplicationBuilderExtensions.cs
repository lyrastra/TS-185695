using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace Moedelo.Common.Audit.Middleware;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Добавить auditTrail middleware в конвейер обработки запросов 
    /// </summary>
    /// <param name="builder">билдер приложения</param>
    /// <param name="excludeRequestBodyUrlPatterns">список шаблонов (regex) url запросов, для которых тело запроса не должно попадать в запись аудита</param>
    /// <param name="excludeResponseBodyUrlPatterns">список шаблонов (regex) url запросов, для которых тело ответа не должно попадать в запись аудита</param>
    /// <param name="excludeUrlPatterns">список шаблонов (regex) url, для которых аудит не ведётся</param>
    /// <param name="treatValidationExceptionAsError"> признак того, что ошибки валидации должны отражаться в аудите как ошибки</param>
    /// <param name="tagClientIpAddress"> признак того, что надо сохранить IP адрес вызывающей стороны в тэги спана аудита</param>
    public static IApplicationBuilder UseAuditApiHandlerTrace(
        this IApplicationBuilder builder,
        IReadOnlyList<string> excludeRequestBodyUrlPatterns = null,
        IReadOnlyList<string> excludeResponseBodyUrlPatterns = null,
        IReadOnlyList<string> excludeUrlPatterns = null,
        bool treatValidationExceptionAsError = false,
        bool tagClientIpAddress = false)
    {
        var settings = new AuditApiHandlerTraceMiddlewareSettings(
            excludeRequestBodyUrlPatterns ?? ArraySegment<string>.Empty,
            excludeResponseBodyUrlPatterns ?? ArraySegment<string>.Empty,
            excludeUrlPatterns ?? ArraySegment<string>.Empty,
            treatValidationExceptionAsError,
            tagClientIpAddress);

        return builder.UseMiddleware<MoedeloAuditTrailMiddleware>(settings);
    }
}