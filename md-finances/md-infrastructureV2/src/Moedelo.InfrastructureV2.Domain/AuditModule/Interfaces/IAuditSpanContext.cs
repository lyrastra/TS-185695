using System;

namespace Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

public interface IAuditSpanContext
{
    Guid AsyncTraceId { get; }

    Guid TraceId { get; }

    Guid? ParentId { get; }

    Guid CurrentId { get; }

    short CurrentDepth { get; }
}
