using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Moedelo.InfrastructureV2.AuditMvc.Internals.Extensions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.AuditMvc;

[InjectAsSingleton(typeof(MvcAuditFilter))]
public sealed class MvcAuditFilter : ActionFilterAttribute
{
    private readonly IAuditTracer auditTracer;

    /// <summary>
    /// Игнорировать "пинги".
    /// Все вызовы, оканчивающиеся на /ping, не будут попадать в аудит
    /// </summary>
    /// <default>true</default>
    private bool ignorePings = true;
        
    /// <summary>
    /// Исключить логирование в auditTrail для этих url
    /// </summary>
    private Regex[] excludeUrls = [];

    /// <summary>
    /// список regex url запросов, для которых тело запроса не должно попадать в запись аудита
    /// </summary>
    private Regex[] excludeRequestBodyForUrls = [];

    /// <summary>
    /// признак того, что ошибки валидации должны отражаться в аудите как ошибки
    /// </summary>
    private bool treatValidationExceptionAsError;

    /// <summary>
    /// добавить IP-адрес клиента
    /// </summary>
    private bool tagClientIpAddress = false;

    /// <summary>
    /// добавить UserAgent клиента
    /// </summary>
    private bool tagUserAgent = false;

    public MvcAuditFilter(IAuditTracer auditTracer)
    {
        this.auditTracer = auditTracer;
    }

    public MvcAuditFilter ExcludeUrls(params Regex[] urlPatterns)
    {
        this.excludeUrls = urlPatterns;

        return this;
    }

    /// Установить список шаблонов (regex) url запросов, для которых тело запроса не должно попадать в запись аудита
    public MvcAuditFilter ExcludeRequestBodyForUrls(IEnumerable<string> urlPatterns)
    {
        this.excludeRequestBodyForUrls = urlPatterns?
            .Select(pattern => new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase))
            .ToArray() ?? [];

        return this;
    }

    /// установить признак того, что ошибки валидации должны отражаться в аудите как ошибки
    public MvcAuditFilter TreatValidationExceptionAsError(bool value)
    {
        this.treatValidationExceptionAsError = value;

        return this;
    }

    /// <summary>
    /// Выставить флаг "Игнорировать пинги"
    /// </summary>
    public MvcAuditFilter IgnorePings(bool value)
    {
        this.ignorePings = value;

        return this;
    }

    public MvcAuditFilter TagClientIpAddress(bool value)
    {
        this.tagClientIpAddress = value;
        return this;
    }

    public MvcAuditFilter TagUserAgent(bool value)
    {
        this.tagUserAgent = value;
        return this;
    }

    private bool MustBeSkipped(ControllerContext filterContext)
    {
        var url = filterContext.HttpContext?.Request.Url?.AbsolutePath;
            
        if (ignorePings && url?.EndsWith("/ping", StringComparison.InvariantCultureIgnoreCase) == true)
        {
            return true;
        }

        if (string.IsNullOrEmpty(url) == false
            && excludeUrls.Length > 0
            && excludeUrls.Any(regex => regex.IsMatch(url)))
        {
            return true;
        }
            
        return false;
    }

    /// <summary>
    /// Вызывается до входа в код метода контроллера
    /// </summary>
    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        var isSkipping = MustBeSkipped(actionContext);

        if (isSkipping)
        {
            return;
        }

        if (!actionContext.IsChildAction)
        {
            auditTracer.StartRootActionAuditTrailSpan(actionContext, excludeRequestBodyForUrls,
                tagClientIpAddress, tagUserAgent);
        }
        else
        {
            auditTracer.StartChildChildAuditTrailSpan(actionContext);
        }
    }

    /// <summary>
    /// Вызывается (всегда если был вызван <see cref="OnActionExecuting"/>) после завершения работы кода метода контроллера
    /// до вызова OnResultExecuting
    /// </summary>
    public override void OnActionExecuted(ActionExecutedContext actionContext)
    {
        var isSkipping = MustBeSkipped(actionContext);

        if (isSkipping)
        {
            return;
        }

        if (actionContext.Exception != null)
        {
            CloseCurrentHttpContextAuditTrailScope(
                actionContext.HttpContext,
                actionContext.Exception,
                actionContext.IsChildAction);
        }
    }

    /// <summary>
    /// Вызывается после <see cref="OnActionExecuted"/> в начале процедуры вычисления результата (ActionResult).
    /// Не вызывается, если при работе метода контроллера возникло исключение
    /// </summary>
    public override void OnResultExecuting(ResultExecutingContext resultContext)
    {
        var isSkipping = MustBeSkipped(resultContext);

        if (isSkipping)
        {
            return;
        }
            
        auditTracer.StartResultExecutingAuditTrailSpan(resultContext);
    }

    /// <summary>
    /// Вызывается, если был вызван <see cref="OnResultExecuting"/> после завершения вычисления результата
    /// </summary>
    public override void OnResultExecuted(ResultExecutedContext resultContext)
    {
        var isSkipping = MustBeSkipped(resultContext);

        if (isSkipping)
        {
            return;
        }

        // закрыть ResultExecuting
        CloseCurrentHttpContextAuditTrailScope(
            resultContext.HttpContext,
            resultContext.Exception,
            resultContext.IsChildAction);
        // закрыть Action
        CloseCurrentHttpContextAuditTrailScope(
            resultContext.HttpContext,
            resultContext.Exception,
            resultContext.IsChildAction);
    }

    private void CloseCurrentHttpContextAuditTrailScope(
        HttpContextBase httpContext,
        Exception exception,
        bool isChildAction)
    {
        httpContext.CloseCurrentHttpContextAuditTrailScope(exception, isChildAction, treatValidationExceptionAsError);
    }
}