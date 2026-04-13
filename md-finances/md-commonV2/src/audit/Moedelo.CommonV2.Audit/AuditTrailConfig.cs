using System.Threading.Tasks;
using Moedelo.CommonV2.Audit.Management;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;

namespace Moedelo.CommonV2.Audit;

/// <summary>
/// Полнофункциональная реализация IAuditTrailConfig 
/// </summary>
[InjectAsSingleton(typeof(IAuditTrailConfig))]
internal sealed class AuditTrailConfig : IAuditTrailConfig
{
    private readonly IAuditTrailTogglesReader togglesReader;

    public AuditTrailConfig(IAuditTrailTogglesReader togglesReader)
    {
        this.togglesReader = togglesReader;
    }

    public string AppName => togglesReader.AppName;

    public bool IsEnabled()
    {
        var toggles = togglesReader.Current;

        return toggles?.IsGloballyEnabled == true;
    }

    public bool IsEnabled(AuditSpanType type)
    {
        var toggles = togglesReader.Current;

        if (toggles?.IsGloballyEnabled != true)
        {
            return false;
        }

        switch (type)
        {
            case AuditSpanType.ApiHandler:
            case AuditSpanType.EventApiHandler:
                return toggles.IsApiHandlerEnabled;
            case AuditSpanType.OutgoingHttpRequest:
                return toggles.IsOutgoingHttpRequestEnabled;
            case AuditSpanType.DbQuery:
            case AuditSpanType.MsSqlDbQuery:
            case AuditSpanType.PostgreSQLDbQuery:
            case AuditSpanType.MySqlDbQuery:
            case AuditSpanType.MongoDbQuery:
            case AuditSpanType.RedisDbQuery:
            case AuditSpanType.RedisDistributedLock:
            case AuditSpanType.CloudProcesses:
            case AuditSpanType.ClickhouseDbQuery:
                return toggles.IsDbQueryEnabled;
            case AuditSpanType.InternalCode:
                return toggles.IsInternalCodeEnabled;
            default:
                return false;
        }
    }

    public Task WaitForReadyAsync()
    {
        return togglesReader.WaitForReadyAsync();
    }
}