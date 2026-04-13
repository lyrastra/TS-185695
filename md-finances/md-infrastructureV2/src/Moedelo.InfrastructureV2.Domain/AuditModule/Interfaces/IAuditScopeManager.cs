namespace Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

public interface IAuditScopeManager
{
    IAuditScope Current { get; }
        
    IAuditScope StartScope(IAuditSpan wrappedSpan);
}