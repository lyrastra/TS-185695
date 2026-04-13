using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Audit.EmptyMock;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;

namespace Moedelo.InfrastructureV2.Audit;

[InjectAsSingleton(typeof(IAuditTracer))]
// ReSharper disable once ClassNeverInstantiated.Global
public sealed class AuditTracer : IAuditTracer
{
    private readonly IAuditTrailConfig trailConfig;
    private readonly IAuditScopeManager scopeManager;
    private readonly IAuditSpanLogger auditSpanLogger;

    public AuditTracer(
        IAuditTrailConfig trailConfig,
        IAuditScopeManager scopeManager,
        IAuditSpanLogger auditSpanLogger)
    {
        this.trailConfig = trailConfig;
        this.scopeManager = scopeManager;
        this.auditSpanLogger = auditSpanLogger;
    }

    public Task WaitForConfigurationReadyAsync()
    {
        return trailConfig.WaitForReadyAsync();
    }

    public bool IsAuditTrailOn()
    {
        return trailConfig.IsEnabled();
    }

    public IAuditSpanBuilder BuildSpan(
        AuditSpanType type, 
        string spanName = null, 
        string memberName = "",
        string sourceFilePath = "", 
        int sourceLineNumber = 0)
    {
        if (trailConfig.IsEnabled(type) == false)
        {
            return EmptySpanBuilder.Instance;
        }

        if (string.IsNullOrWhiteSpace(spanName))
        {
            spanName = $"func {memberName} from {sourceFilePath} file at {sourceLineNumber} line";
        }

        var appName = trailConfig.AppName;
                
        return new AuditSpanBuilder(this, scopeManager, type, appName, spanName);
    }
        
    internal void AddFinishedSpan(IAuditSpan auditSpan)
    {
        auditSpanLogger.FireAndForgetLog(auditSpan);
    }
}