using Moedelo.BankIntegrations.Utils.Framework.Abstractions.Redis;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.RedisDataAccess.Common;
using IRedisDbExecutor = Moedelo.Infrastructure.Redis.Abstractions.Interfaces.IRedisDbExecuter;

namespace Moedelo.BankIntegrations.Utils.Framework.Redis
{
    [InjectAsSingleton(typeof(IBankIntegrationsRedisDbExecutor))]
    internal sealed class BankIntegrationsRedisDbExecutor : RedisDbExecuterBase, IBankIntegrationsRedisDbExecutor
    {
        public BankIntegrationsRedisDbExecutor(
            ISettingRepository settingRepository,
            IRedisDbExecutor redisDbExecutor,
            IAuditTracer auditTracer)
            : base(
                redisDbExecutor,
                settingRepository,
                settingRepository.Get("RedisConnection"),
                settingRepository.Get("IntegrationRedisDbNumber").GetIntValue(),
                auditTracer)
        {}
    }
}