using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.Audit.Configuration.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Audit.Configuration
{
    [InjectAsSingleton(typeof(IAuditConfig))]
    internal sealed class AuditConfig : IAuditConfig
    {
        private readonly ICommandStateManager commandStateManager;

        public AuditConfig(
            ICommandStateManager commandStateManager)
        {
            this.commandStateManager = commandStateManager;
        }

        public string AppName => commandStateManager.AuditTrailAppName;

        public bool IsEnabled()
        {
            var currentState = commandStateManager.Current;

            return currentState is { Enabled: true };
        }

        public bool IsEnabled(AuditSpanType type)
        {
            var currentState = commandStateManager.Current;

            if (currentState == null || currentState.Enabled == false)
            {
                return false;
            }

            switch (type)
            {
                case AuditSpanType.ApiHandler:
                case AuditSpanType.EventApiHandler:
                    return currentState.EnabledApiHandler;
                case AuditSpanType.OutgoingHttpRequest:
                case AuditSpanType.ConsulHttpCall:
                    return currentState.EnabledOutgoingHttpRequest;
                case AuditSpanType.DbQuery:
                case AuditSpanType.MsSqlDbQuery:
                case AuditSpanType.PostgreSQLDbQuery:
                case AuditSpanType.MySqlDbQuery:
                case AuditSpanType.MongoDbQuery:
                case AuditSpanType.RedisDbQuery:
                case AuditSpanType.RedisDistributedLock:
                case AuditSpanType.CloudProcesses:
                case AuditSpanType.ClickhouseDbQuery:
                    return currentState.EnabledDbQuery;
                case AuditSpanType.InternalCode:
                    return currentState.EnabledInternalCode;
                default:
                    return false;
            }
        }
    }
}