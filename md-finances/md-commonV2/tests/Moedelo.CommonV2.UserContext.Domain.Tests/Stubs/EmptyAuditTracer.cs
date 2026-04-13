using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;
using Moq;

namespace Moedelo.CommonV2.UserContext.Domain.Tests.Stubs;

[InjectAsSingleton(typeof(IAuditTracer))]
internal sealed class EmptyAuditTracer : IAuditTracer
{
    public Task WaitForConfigurationReadyAsync() => Task.CompletedTask;

    public bool IsAuditTrailOn() => false;

    public IAuditSpanBuilder BuildSpan(AuditSpanType type, string spanName = null, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return Mock.Of<IAuditSpanBuilder>();
    }
}
