using System;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.Audit.EmptyMock;

internal sealed class EmptyAuditSpanContext : IAuditSpanContext
{
    public static EmptyAuditSpanContext Instance { get; } = new();

    private EmptyAuditSpanContext() { }

    public Guid AsyncTraceId { get; } = Guid.Empty;

    public Guid TraceId { get; } = Guid.Empty;

    public Guid? ParentId { get; } = Guid.Empty;

    public Guid CurrentId { get; } = Guid.Empty;

    public short CurrentDepth { get; } = 0;
}