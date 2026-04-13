using Moedelo.BankIntegrations.Utils.Abstractions.Redis;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Redis.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Redis.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.Utils.Redis
{
    [InjectAsSingleton(typeof(IDefaultRedisDbExecutor))]
    public sealed class BankIntegrationsRedisDbExecutor: MoedeloRedisDbExecutorBase, IDefaultRedisDbExecutor
    {
        public BankIntegrationsRedisDbExecutor(IRedisDbExecuter redisDbExecuter, ISettingRepository settingRepository, IAuditTracer auditTracer)
            : base(redisDbExecuter,
                settingRepository,
                settingRepository.Get("RedisConnection"),
                settingRepository.Get("IntegrationRedisDbNumber"),
                auditTracer)
        {
        }
    }
}