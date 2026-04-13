using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moq;

namespace Moedelo.CommonV2.UserContext.Domain.Tests.Stubs;

[InjectAsSingleton(typeof(IAuditScopeManager))]
internal sealed class EmptyAuditScopeManager : IAuditScopeManager
{
    public IAuditScope Current { get; } = null;
    public IAuditScope StartScope(IAuditSpan wrappedSpan)
    {
        return Mock.Of<IAuditScope>();
    }
}
