using Moedelo.Common.Audit.Abstractions.Interfaces;

namespace Moedelo.Common.Audit
{
    internal sealed class AuditScope : IAuditScope
    {
        private readonly AuditScopeManager auditScopeManager;

        private readonly IAuditScope parentScope;

        public AuditScope(AuditScopeManager auditScopeManager, IAuditSpan wrappedSpan)
        {
            this.auditScopeManager = auditScopeManager;
            this.Span = wrappedSpan;

            parentScope = auditScopeManager.Current;
            auditScopeManager.Current = this;
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

            auditScopeManager.Current = parentScope;
        }
    }
}