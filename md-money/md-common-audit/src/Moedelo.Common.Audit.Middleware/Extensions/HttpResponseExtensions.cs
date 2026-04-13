using Microsoft.AspNetCore.Http;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Middleware.Internals;

namespace Moedelo.Common.Audit.Middleware.Extensions;

internal static class HttpResponseExtensions
{
    internal static void AddAuditTrailHeaders(this HttpResponse response, IAuditSpan span)
    {
        try
        {
            const string headerName = CustomHeaderNames.AuditTrailContext;

            if (response.Headers.ContainsKey(headerName) == false)
            {
                var date = span.StartDateUtc.Date;
                var ctx = span.Context;
                var headerValue = $"00;{date:yyyyMMdd};{ctx.AsyncTraceId};{ctx.TraceId};{ctx.CurrentId}";
                response.Headers[headerName] = headerValue;
            }
        }
        catch
        {
            /* не имеем права падать по этой причине */
        }
    }
}
