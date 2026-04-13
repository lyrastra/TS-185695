using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.Audit.EmptyMock;

internal class EmptyAuditScope : IAuditScope
{
    public static readonly EmptyAuditScope Instance = new();

    private EmptyAuditScope() { }

    public IAuditSpan Span { get; } = EmptyAuditSpan.Instance;

    public bool IsEnabled => false;

    public void Dispose()
    {
    }
}
