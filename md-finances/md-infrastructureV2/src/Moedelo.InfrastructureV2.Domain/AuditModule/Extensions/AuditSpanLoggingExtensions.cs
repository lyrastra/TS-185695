using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.Domain.AuditModule.Extensions;

public static class AuditSpanLoggingExtensions
{
    public static IEnumerable<KeyValuePair<string, object>> EnumerateLogExtraEventFields(this IAuditSpan auditSpan)
    {
        if (auditSpan == null)
        {
            yield break;
        }
            
        var ctx = auditSpan.Context;
            
        if (ctx == null)
        {
            yield return new("SpanName", auditSpan.Name);
            yield break;
        }

        yield return new("SpanName", auditSpan.Name);
        yield return new("AsyncTraceId", ctx.AsyncTraceId.ToString());
        yield return new("TraceId", ctx.TraceId.ToString());
        yield return new("CurrentGuid", ctx.CurrentId.ToString());
        if (ctx.ParentId.HasValue)
        {
            yield return new("ParentGuid", ctx.ParentId.ToString());
        }
        var linkPath = $"/ui/call-tree?asyncTraceId={ctx.AsyncTraceId}&traceId={ctx.TraceId}&date={auditSpan.StartDateUtc.Date:yyyy-MM-ddT00:00:00Z}";
        yield return new("MdAuditLinkPath", linkPath);
    }
}
