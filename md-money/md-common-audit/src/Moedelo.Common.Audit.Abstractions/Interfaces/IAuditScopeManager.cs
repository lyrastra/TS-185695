namespace Moedelo.Common.Audit.Abstractions.Interfaces
{
    public interface IAuditScopeManager
    {
        IAuditScope Current { get; }
        
        IAuditScope StartScope(IAuditSpan wrappedSpan);
    }
}