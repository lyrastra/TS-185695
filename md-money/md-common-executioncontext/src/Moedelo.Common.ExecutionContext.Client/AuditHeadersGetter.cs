using System;
using System.Collections.Generic;
using Moedelo.Common.Audit.Abstractions.Helpers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.ExecutionContext.Client
{
    [InjectAsSingleton(typeof(IAuditHeadersGetter))]
    internal sealed class AuditHeadersGetter : IAuditHeadersGetter
    {
        private readonly IAuditScopeManager auditScopeManager;

        public AuditHeadersGetter(IAuditScopeManager auditScopeManager)
        {
            this.auditScopeManager = auditScopeManager;
        }

        public IReadOnlyCollection<KeyValuePair<string, string>> GetHeaders()
        {
            var auditSpanContext = auditScopeManager.Current?.Span?.Context;

            if (auditSpanContext != null)
            {
                return new[]
                {
                    new KeyValuePair<string, string>(AuditHeaderParamHelper.ParentScopeContext,
                        auditSpanContext.ToJsonString()),
                };
            }

            return Array.Empty<KeyValuePair<string, string>>();
        }
    }
}