using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Moedelo.InfrastructureV2.AuditWebApi.Extensions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Extensions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.AuditWebApi;

[InjectAsTransient(typeof(AuditTrailWebApiFilter))]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class AuditTrailWebApiFilter : Attribute, IActionFilter
{
    private readonly IAuditTracer auditTracer;

    #region Setup

    /// <summary>
    /// Игнорировать "пинги".
    /// Все вызовы, оканчивающиеся на /ping, не будут попадать в аудит
    /// </summary>
    /// <default>true</default>
    private bool ignorePings = true;

    /// <summary>
    /// список regex url запросов, для которых тело запроса не должно попадать в запись аудита
    /// </summary>
    private Regex[] excludeRequestBodyForUrls = Array.Empty<Regex>();

    /// <summary>
    /// список regex url запросов, для которых тело ответа не должно попадать в запись аудита
    /// </summary>
    private Regex[] excludeResponseBodyForUrls = Array.Empty<Regex>();

    /// <summary>
    /// признак того, что ошибки валидации должны отражаться в аудите как ошибки
    /// </summary>
    private bool treatValidationExceptionAsError = false;

    /// <summary>
    /// добавить IP-адрес клиента
    /// </summary>
    private bool tagClientIpAddress = false;
    
    /// <summary>
    /// Добавить UserAgent клиента
    /// </summary>
    private bool tagClientUserAgent = false;

    public AuditTrailWebApiFilter(IAuditTracer auditTracer)
    {
        this.auditTracer = auditTracer;
    }

    /// <summary>
    /// Установить список шаблонов (regex) url запросов, для которых тело запроса не должно попадать в запись аудита
    /// </summary>
    public AuditTrailWebApiFilter ExcludeRequestBodyForUrls(IEnumerable<string> urlPatterns)
    {
        this.excludeRequestBodyForUrls = urlPatterns?
            .Select(pattern => new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase, TimeSpan.FromSeconds(1)))
            .ToArray() ?? Array.Empty<Regex>();

        return this;
    }

    /// <summary>
    /// Установить список шаблонов (regex) url запросов, для которых тело ответа не должно попадать в запись аудита
    /// </summary>
    public AuditTrailWebApiFilter ExcludeResponseBodyForUrls(IEnumerable<string> urlPatterns)
    {
        this.excludeResponseBodyForUrls = urlPatterns?
            .Select(pattern => new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase, TimeSpan.FromSeconds(1)))
            .ToArray() ?? Array.Empty<Regex>();

        return this;
    }

    /// <summary>
    /// Установить признак того, что ошибки валидации должны отражаться в аудите как ошибки
    /// </summary>
    public AuditTrailWebApiFilter TreatValidationExceptionAsError(bool value)
    {
        this.treatValidationExceptionAsError = value;

        return this;
    }

    /// <summary>
    /// Выставить флаг "Игнорировать пинги"
    /// </summary>
    public AuditTrailWebApiFilter IgnorePings(bool value)
    {
        this.ignorePings = value;

        return this;
    }

    public AuditTrailWebApiFilter TagClientIpAddress(bool value)
    {
        this.tagClientIpAddress = value;
        return this;
    }

    public AuditTrailWebApiFilter TagClientUserAgent(bool value)
    {
        this.tagClientUserAgent = value;
        return this;
    }

    #endregion

    #region Private

    private static IAuditSpanContext GetNonEmptyParentContext(HttpRequestMessage httpContext)
    {
        var parentContext = GetParentContext(httpContext);

        return parentContext.IsEmpty() ? null : parentContext;
    }

    private static IAuditSpanContext GetParentContext(HttpRequestMessage httpContext)
    {
        try
        {
            var parentScopeContextJson = GetParentScopeContextJson(httpContext.Headers);
            return string.IsNullOrWhiteSpace(parentScopeContextJson)
                ? null
                : parentScopeContextJson.FromJsonString<ContextFromRequest>();
        }
        catch
        {
            return null;
        }
    }

    private static string GetParentScopeContextJson(HttpHeaders headers)
    {
        var hasHeader = headers.TryGetValues(AuditHeaderParam.ParentScopeContext, out var parentScopeContextHeaders);
        var requestId = hasHeader ? parentScopeContextHeaders.First() : null;

        return requestId;
    }

    private bool IsRequestTraceable(HttpRequestMessage httpRequestMessage)
    {
        return !(ignorePings &&
               httpRequestMessage.RequestUri.AbsolutePath?.EndsWith("/ping", StringComparison.InvariantCultureIgnoreCase) == true);
    }

    private Task DumpRequestAsync(IAuditSpan span, HttpRequestMessage httpRequestMessage)
    {
        var uri = httpRequestMessage.RequestUri?.OriginalString ?? "";
        var includeRequestBody = excludeRequestBodyForUrls
            .Any(regex => regex.IsMatch(uri)) == false;

        return span.TryAddRequestAsync(httpRequestMessage, includeRequestBody, tagClientIpAddress, tagClientUserAgent);
    }
    
    private Task DumpResponseAsync(IAuditSpan span, HttpRequestMessage httpRequestMessage, HttpResponseMessage response)
    {
        var uri = httpRequestMessage.RequestUri?.OriginalString ?? "";
        var includeResponseBody = excludeResponseBodyForUrls
            .Any(regex => regex.IsMatch(uri)) == false;

        return span.TryAddResponseAsync(response, includeResponseBody, treatValidationExceptionAsError);
    }

    private IAuditScope StartAuditTrailScope(HttpRequestMessage httpRequestMessage)
    {
        var parentContext = GetNonEmptyParentContext(httpRequestMessage);

        var spanName = httpRequestMessage.RequestUri.GetAuditSpanName(httpRequestMessage.Method.Method);

        var scope = auditTracer.BuildSpan(AuditSpanType.ApiHandler, spanName)
            .AsChildOf(parentContext)
            .Start();

        var normalizedSpanName = httpRequestMessage.GetAuditSpanNameFromRouteTemplate();

        if (normalizedSpanName != null)
        {
            scope.Span.SetNormalizedName(normalizedSpanName);
        }

        return scope;
    }

    private bool ShouldMarkAsError(Exception exception)
    {
        if (exception is OperationCanceledException)
        {
            // отмену не считаем ошибкой
            return false;
        }

        var isValidation = exception.GetType().Name == "ValidationFailureException";
        
        if (isValidation)
        {
            // на усмотрение пользователя
            return treatValidationExceptionAsError;
        }
        
        // всё остальное считаем ошибкой
        return true;
    }

    #endregion


    public Task<HttpResponseMessage> ExecuteActionFilterAsync(
        HttpActionContext actionContext,
        CancellationToken cancellationToken,
        Func<Task<HttpResponseMessage>> continuation)
    {
        var request = actionContext.Request;

        if (IsRequestTraceable(request) == false)
        {
            return continuation();
        }

        return RunInAuditTrailScopeAsync(continuation, request);
    }

    private async Task<HttpResponseMessage> RunInAuditTrailScopeAsync(Func<Task<HttpResponseMessage>> continuation,
        HttpRequestMessage request)
    {
        await auditTracer.WaitForConfigurationReadyAsync().ConfigureAwait(true);

        if (auditTracer.IsAuditTrailOn() == false)
        {
            return await continuation().ConfigureAwait(true);
        }

        using var scope = StartAuditTrailScope(request);
        
        request.AddAuditTrailSpan(scope.Span);

        HttpResponseMessage response = null;

        try
        {
            await DumpRequestAsync(scope.Span, request).ConfigureAwait(true);

            response = await continuation().ConfigureAwait(true);

            await DumpResponseAsync(scope.Span, request, response).ConfigureAwait(true);

            return response;
        }
        catch (Exception exception)
        {
            if (ShouldMarkAsError(exception))
            {
                scope.Span.SetError(exception);
            }

            throw;
        }
        finally
        {
            // Добавляем заголовок аудита всегда (при успехе и при ошибках)
            response?.AddAuditTrailResponseHeader(scope.Span);
        }
    }

    public bool AllowMultiple => false;
}
