using System.Threading;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Audit
{
    [InjectAsSingleton(typeof(IAuditScopeManager))]
    internal sealed class AuditScopeManager : IAuditScopeManager
    {
        private static readonly AsyncLocal<IAuditScope> current = new AsyncLocal<IAuditScope>();

        public IAuditScope Current
        {
            get => current.Value;
            set => current.Value = value;
        }
        
        public IAuditScope StartScope(IAuditSpan wrappedSpan)
        {
            return new AuditScope(this, wrappedSpan);
        }
    }
}