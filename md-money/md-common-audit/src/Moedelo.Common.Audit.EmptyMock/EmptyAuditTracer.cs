using System;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;

namespace Moedelo.Common.Audit.EmptyMock;

public sealed class EmptyAuditTracer : IAuditTracer
{
    private static readonly Lazy<EmptyAuditTracer> Lazy =
        new Lazy<EmptyAuditTracer>(() => new EmptyAuditTracer());

    private EmptyAuditTracer()
    {
    }

    public static EmptyAuditTracer Instance => Lazy.Value;

    public IAuditSpanBuilder BuildSpan(
        AuditSpanType type,
        string spanName = null,
        string memberName = "",
        string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return EmptySpanBuilder.Instance;
    }
}
