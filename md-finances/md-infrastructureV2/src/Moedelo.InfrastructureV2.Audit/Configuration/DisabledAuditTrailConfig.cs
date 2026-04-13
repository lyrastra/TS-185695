using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;

namespace Moedelo.InfrastructureV2.Audit.Configuration;

/// <summary>
/// Заглушка-пустышка для IAuditTrailConfig
/// </summary>
[InjectAsSingleton(typeof(IAuditTrailConfig))]
internal sealed class DisabledAuditTrailConfig : IAuditTrailConfig
{
    public string AppName => string.Empty;

    public bool IsEnabled() => false;

    public bool IsEnabled(AuditSpanType type) => false;

    public Task WaitForReadyAsync() => Task.CompletedTask;
}