using System.Collections.Generic;

namespace Moedelo.Common.Audit.Middleware;

/// <summary>
/// Набор параметров для настройки <see cref="MoedeloAuditTrailMiddleware"/>
/// </summary>
/// <param name="ExcludeRequestBodyUrlPatterns">список шаблонов (regex) url запросов, для которых тело запроса не должно попадать в запись аудита</param>
/// <param name="ExcludeResponseBodyUrlPatterns">список шаблонов (regex) url запросов, для которых тело ответа не должно попадать в запись аудита</param>
/// <param name="ExcludeUrlPatterns">список шаблонов (regex) url, для которых аудит не ведётся</param>
/// <param name="TreatValidationExceptionAsError"> признак того, что ошибки валидации должны отражаться в аудите как ошибки</param>
/// <param name="TagClientIpAddress"> признак того, что надо сохранить IP адрес вызывающей стороны в тэги спана аудита</param>
internal sealed record AuditApiHandlerTraceMiddlewareSettings(
    IReadOnlyCollection<string> ExcludeRequestBodyUrlPatterns,
    IReadOnlyCollection<string> ExcludeResponseBodyUrlPatterns,
    IReadOnlyCollection<string> ExcludeUrlPatterns,
    bool TreatValidationExceptionAsError,
    bool TagClientIpAddress = false);
