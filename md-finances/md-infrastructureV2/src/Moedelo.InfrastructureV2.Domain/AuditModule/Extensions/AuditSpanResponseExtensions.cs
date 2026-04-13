using System.Linq;
using System.Net.Http;
using System.Web;
using Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.Domain.AuditModule.Extensions;

/// <summary>
/// Расширения для работы с заголовками ответа аудита
/// </summary>
public static class AuditSpanResponseExtensions
{
    /// <summary>
    /// Генерирует значение заголовка MD-AuditTrail-Context для передачи в ответе
    /// </summary>
    /// <param name="span">Спан аудита</param>
    /// <returns>Строка заголовка в формате: 00;YYYYMMDD;AsyncTraceId;TraceId;CurrentId</returns>
    public static string GenerateAuditTrailHeaderValue(this IAuditSpan span)
    {
        var date = span.StartDateUtc.Date;
        var ctx = span.Context;
        return $"00;{date:yyyyMMdd};{ctx.AsyncTraceId};{ctx.TraceId};{ctx.CurrentId}";
    }

    public static void AddAuditTrailSpan(this HttpRequestMessage request, IAuditSpan span)
    {
        request.Properties.Add(AuditHttpRequestProperties.AuditTrailSpan, span);
    }

    /// <summary>
    /// Добавляет заголовок MD-AuditTrail-Context в Web API ответ
    /// </summary>
    /// <param name="response">HTTP ответ Web API</param>
    /// <param name="span">Спан аудита</param>
    public static void AddAuditTrailResponseHeader(this HttpResponseMessage response, IAuditSpan span)
    {
        try
        {
            if (!response.Headers.Contains(AuditHeaderParam.AuditTrailContext))
            {
                var headerValue = span.GenerateAuditTrailHeaderValue();
                response.Headers.Add(AuditHeaderParam.AuditTrailContext, headerValue);
            }
        }
        catch
        {
            // Не имеем права падать по этой причине
        }
    }

    /// <summary>
    /// Добавляет заголовок MD-AuditTrail-Context в MVC ответ
    /// </summary>
    /// <param name="response">HTTP ответ MVC</param>
    /// <param name="span">Спан аудита</param>
        public static void AddAuditTrailResponseHeader(this HttpResponseBase response, IAuditSpan span)
        {
        try
        {
            if (!response.Headers.AllKeys.Contains(AuditHeaderParam.AuditTrailContext))
            {
                var headerValue = span.GenerateAuditTrailHeaderValue();
                response.Headers.Add(AuditHeaderParam.AuditTrailContext, headerValue);
            }
        }
        catch
        {
            // Не имеем права падать по этой причине
        }
    }
}
