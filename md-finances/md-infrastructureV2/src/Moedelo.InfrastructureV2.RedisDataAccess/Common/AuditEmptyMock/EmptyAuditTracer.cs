using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;

namespace Moedelo.InfrastructureV2.RedisDataAccess.Common.AuditEmptyMock;

public sealed class EmptyAuditTracer : IAuditTracer
{
    private EmptyAuditTracer()
    {
    }

    public static readonly EmptyAuditTracer Instance = new EmptyAuditTracer();

    public Task WaitForConfigurationReadyAsync() => Task.CompletedTask;

    public bool IsAuditTrailOn() => false;

    public IAuditSpanBuilder BuildSpan(AuditSpanType type, string spanName = null, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0) => EmptySpanBuilder.Instance;
}