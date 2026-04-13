using Moedelo.BankIntegrations.Utils.Abstractions.Redis;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Redis.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Redis.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.Utils.Redis
{

    [InjectAsSingleton(typeof(IBankIntegrationsCacheRedisDbExecutor))]
    internal sealed class BankIntegrationsCacheRedisDbExecutor : MoedeloRedisDbExecutorBase,
        IBankIntegrationsCacheRedisDbExecutor
    {
        public BankIntegrationsCacheRedisDbExecutor(
            IRedisDbExecuter redisDbExecutor,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer)
            : base(
                redisDbExecutor,
                settingRepository,
                settingRepository.Get("RedisCacheConnection"),
                settingRepository.Get("IntegrationCacheRedisDbNumber"),
                auditTracer)
        {
        }
    }
}
