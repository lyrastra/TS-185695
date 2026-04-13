using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.AuditModule.Extensions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.InfrastructureV2.Audit.MdLogging;

internal sealed class AuditTrailLogEventExtender : ILogEventExtender
{
    private readonly IAuditScopeManager auditScopeManager;

    public AuditTrailLogEventExtender(IAuditScopeManager auditScopeManager)
    {
        this.auditScopeManager = auditScopeManager;
    }

    public IEnumerable<KeyValuePair<string, object>> EnumerateLogExtraEventFields()
    {
        return auditScopeManager.Current?.Span?.EnumerateLogExtraEventFields() ?? [];
    }
}
