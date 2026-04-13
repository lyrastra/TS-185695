using System.Threading;
using Moedelo.InfrastructureV2.Audit.MdLogging;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.InfrastructureV2.Audit;

[InjectAsSingleton(typeof(IAuditScopeManager))]
public sealed class AuditScopeManager : IAuditScopeManager
{
    private static readonly AsyncLocal<IAuditScope> current = new AsyncLocal<IAuditScope>();

    public AuditScopeManager(ILogger logger)
    {
        logger.AddLogEventExtender(new AuditTrailLogEventExtender(this));
    }

    public IAuditScope Current => current.Value;

    public IAuditScope StartScope(IAuditSpan wrappedSpan)
    {
        return new AuditScope(this, wrappedSpan);
    }
    
    internal void SetCurrent(IAuditScope value)
    {
        current.Value = value;
    }

}