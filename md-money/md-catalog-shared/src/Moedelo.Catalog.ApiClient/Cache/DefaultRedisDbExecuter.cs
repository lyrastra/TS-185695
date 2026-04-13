using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Redis.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Redis.Abstractions.Interfaces;

namespace Moedelo.Catalog.ApiClient.Cache
{
    [InjectAsSingleton(typeof(DefaultRedisDbExecuter))]
    internal sealed class DefaultRedisDbExecuter : MoedeloRedisDbExecutorBase
    {
        public DefaultRedisDbExecuter(
            IRedisDbExecuter redisDbExecuter,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer)
            : base(
                redisDbExecuter,
                settingRepository,
                settingRepository.Get("RedisConnection"),
                SettingValue.CreateConstSettingValue("0"),
                auditTracer)
        {
        }
    }
}