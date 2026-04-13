using System;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.Audit
{
    public class AuditSpanContext : IAuditSpanContext
    {
        public static AuditSpanContext New()
        {
            return new AuditSpanContext
            {
                AsyncTraceId = Guid.NewGuid(),
                TraceId = Guid.NewGuid(),
                ParentId = null,
                CurrentId = Guid.NewGuid(),
                CurrentDepth = 0,
            };
        }
        
        public static AuditSpanContext ChildOf(IAuditSpanContext parent, bool ignoreTraceId = false)
        {
            return new AuditSpanContext
            {
                AsyncTraceId = parent.AsyncTraceId,
                TraceId = ignoreTraceId ? Guid.NewGuid() : parent.TraceId,
                ParentId = parent.CurrentId,
                CurrentId = Guid.NewGuid(),
                CurrentDepth = (short)(parent.CurrentDepth + 1),
            };
        }
        
        public Guid AsyncTraceId { get; private set; }
        
        public Guid TraceId { get; private set; }
        
        public Guid? ParentId { get; private set; }
        
        public Guid CurrentId { get; private set; }
        
        public short CurrentDepth { get; private set; }
    }
}