using System.Collections.Generic;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Logging.ExtraLog.Abstractions;

namespace Moedelo.Common.Logging.ExtraLog.Audit
{
    internal sealed class AuditSpanContextExtraLogFieldsProvider : IExtraLogFieldsProvider
    {
        private readonly IAuditScopeManager auditScopeManager;

        public AuditSpanContextExtraLogFieldsProvider(IAuditScopeManager auditScopeManager)
        {
            this.auditScopeManager = auditScopeManager;
        }

        public IEnumerable<ExtraLogField> Get()
        {
            var auditSpan = auditScopeManager.Current?.Span;

            if (auditSpan == null)
            {
                yield break;
            }
            
            var ctx = auditSpan.Context;
            
            if (ctx == null)
            {
                yield return new ExtraLogField("SpanName", auditSpan.Name);
                yield break;
            }

            yield return new ExtraLogField("SpanName", auditSpan.Name);
            yield return new ExtraLogField("AsyncTraceId", ctx.AsyncTraceId.ToString());
            yield return new ExtraLogField("TraceId", ctx.TraceId.ToString());
            yield return new ExtraLogField("CurrentGuid", ctx.CurrentId.ToString());
            if (ctx.ParentId.HasValue)
            {
                yield return new ExtraLogField("ParentGuid", ctx.ParentId.ToString());
            }
            var linkPath = $"/ui/call-tree?asyncTraceId={ctx.AsyncTraceId}&traceId={ctx.TraceId}&date={auditSpan.StartDateUtc.Date:yyyy-MM-ddT00:00:00Z}";
            yield return new ExtraLogField("MdAuditLinkPath", linkPath);
        }
    }
}