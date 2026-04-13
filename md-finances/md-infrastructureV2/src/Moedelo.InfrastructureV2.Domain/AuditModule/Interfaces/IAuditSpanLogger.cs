namespace Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

public interface IAuditSpanLogger
{
    void FireAndForgetLog(IAuditSpan span);
}