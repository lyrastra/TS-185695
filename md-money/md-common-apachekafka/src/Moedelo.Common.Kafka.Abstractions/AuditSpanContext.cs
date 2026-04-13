using System;
using Moedelo.Common.Audit.Abstractions.Interfaces;

namespace Moedelo.Common.Kafka.Abstractions
{
    public sealed class AuditSpanContext : IAuditSpanContext
    {

        public AuditSpanContext()
        {
        }
        
        public AuditSpanContext(IAuditSpanContext auditSpanContext)
        {
            AsyncTraceId = auditSpanContext.AsyncTraceId;
            TraceId = auditSpanContext.TraceId;
            ParentId = auditSpanContext.ParentId;
            CurrentId = auditSpanContext.CurrentId;
            CurrentDepth = auditSpanContext.CurrentDepth;
        }

        public Guid AsyncTraceId { get; set; }
        
        public Guid TraceId { get; set; }
        
        public Guid? ParentId { get; set; }

        public Guid CurrentId { get; set; }
        
        public short CurrentDepth { get; set; }
    }
}