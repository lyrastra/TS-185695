using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.Audit.Configuration.Interfaces;
using Moedelo.Common.Audit.EmptyMock;
using Moedelo.Common.Audit.Logging.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Audit
{
    [InjectAsSingleton(typeof(IAuditTracer))]
    internal sealed class AuditTracer : IAuditTracer
    {
        private readonly IAuditConfig config;
        
        private readonly IAuditScopeManager scopeManager;

        private readonly IAuditSpanLogger auditSpanLogger;
        
        public AuditTracer(IAuditConfig config, IAuditScopeManager scopeManager, IAuditSpanLogger auditSpanLogger)
        {
            this.config = config;
            this.scopeManager = scopeManager;
            this.auditSpanLogger = auditSpanLogger;
        }

        public IAuditSpanBuilder BuildSpan(
            AuditSpanType type,
            string spanName = null,
            string memberName = "",
            string sourceFilePath = "",
            int sourceLineNumber = 0)
        {
            if (config.IsEnabled(type) == false)
            {
                return EmptySpanBuilder.Instance;
            }

            if (string.IsNullOrWhiteSpace(spanName))
            {
                spanName = $"func {memberName} from {sourceFilePath} file at {sourceLineNumber} line";
            }

            var appName = config.AppName;
                
            return new AuditSpanBuilder(this, scopeManager, type, appName, spanName);
        }
        
        internal void AddFinishedSpan(IAuditSpan auditSpan)
        {
            auditSpanLogger.FireAndForgetLog(auditSpan);
        }
    }
}