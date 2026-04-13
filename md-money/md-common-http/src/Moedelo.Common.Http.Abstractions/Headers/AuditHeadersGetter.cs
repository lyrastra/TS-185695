using System.Collections.Generic;
using Moedelo.Common.Audit.Abstractions.Helpers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Http.Abstractions.Headers
{
    [InjectAsSingleton(typeof(IAuditHeadersGetter))]
    internal sealed class AuditHeadersGetter : IAuditHeadersGetter
    {
        private readonly IAuditScopeManager auditScopeManager;

        public AuditHeadersGetter(IAuditScopeManager auditScopeManager)
        {
            this.auditScopeManager = auditScopeManager;
        }

        public IEnumerable<KeyValuePair<string, string>> EnumerateHeaders()
        {
            var auditSpanContext = auditScopeManager.Current?.Span?.Context;

            if (auditSpanContext != null)
            {
                yield return new KeyValuePair<string, string>(
                    AuditHeaderParamHelper.ParentScopeContext,
                    auditSpanContext.ToJsonString());
            }
        }
    }
}