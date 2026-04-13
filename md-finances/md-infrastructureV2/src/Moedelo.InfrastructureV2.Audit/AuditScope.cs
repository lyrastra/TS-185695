using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.Audit
{
    public class AuditScope : IAuditScope
    {
        private readonly AuditScopeManager auditScopeManager;
        private readonly IAuditScope parentScope;

        public AuditScope(AuditScopeManager auditScopeManager, IAuditSpan wrappedSpan)
        {
            this.auditScopeManager = auditScopeManager;
            this.Span = wrappedSpan;

            parentScope = auditScopeManager.Current;
            auditScopeManager.SetCurrent(this);
        }

        public IAuditSpan Span { get; }

        public bool IsEnabled => true;

        public void Dispose()
        {
            if (auditScopeManager.Current != this)
            {
                return;
            }
            
            Span.Finish();

            auditScopeManager.SetCurrent(parentScope);
        }
    }
}