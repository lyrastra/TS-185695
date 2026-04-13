using System;
using Moedelo.Common.Audit.Abstractions.Interfaces;

namespace Moedelo.Common.Audit.EmptyMock;

public sealed class EmptyAuditScope : IAuditScope
{
    private static readonly Lazy<EmptyAuditScope> Lazy =
        new Lazy<EmptyAuditScope>(() => new EmptyAuditScope());

    private EmptyAuditScope()
    {
    }

    public static EmptyAuditScope Instance => Lazy.Value;

    public IAuditSpan Span { get; } = EmptyAuditSpan.Instance;

    public bool IsEnabled => false;

    public void Dispose()
    {
    }
}
