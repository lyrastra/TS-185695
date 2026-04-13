using System;
using Moedelo.Common.Audit.Abstractions.Interfaces;

namespace Moedelo.Common.Audit.EmptyMock;

public sealed class EmptyAuditSpanContext : IAuditSpanContext
{
    private static readonly Lazy<EmptyAuditSpanContext> Lazy =
        new Lazy<EmptyAuditSpanContext>(() => new EmptyAuditSpanContext());

    private EmptyAuditSpanContext()
    {
    }

    public static EmptyAuditSpanContext Instance => Lazy.Value;

    public Guid AsyncTraceId => Guid.Empty;

    public Guid TraceId => Guid.Empty;

    public Guid? ParentId => Guid.Empty;

    public Guid CurrentId => Guid.Empty;

    public short CurrentDepth => 0;
}
