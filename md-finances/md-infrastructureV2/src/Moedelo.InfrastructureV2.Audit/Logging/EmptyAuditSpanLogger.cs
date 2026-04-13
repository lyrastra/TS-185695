using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.Audit.Logging
{
    [InjectAsSingleton(typeof(IAuditSpanLogger))]
    public class EmptyAuditSpanLogger : IAuditSpanLogger
    {
        public void FireAndForgetLog(IAuditSpan span)
        {
            //ignore
        }
    }
}