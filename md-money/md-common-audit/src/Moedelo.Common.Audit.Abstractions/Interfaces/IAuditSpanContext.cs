using System;

namespace Moedelo.Common.Audit.Abstractions.Interfaces
{
    public interface IAuditSpanContext
    {
        Guid AsyncTraceId { get; }
        
        Guid TraceId { get; }
        
        Guid? ParentId { get; }
        
        Guid CurrentId { get; }
        
        short CurrentDepth { get; }
    }
}