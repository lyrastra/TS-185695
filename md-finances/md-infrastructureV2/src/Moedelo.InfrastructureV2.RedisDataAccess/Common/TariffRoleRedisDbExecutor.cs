using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.RedisDataAccess.Models;
using IRedisDbExecutor = Moedelo.Infrastructure.Redis.Abstractions.Interfaces.IRedisDbExecuter;

namespace Moedelo.InfrastructureV2.RedisDataAccess.Common
{
    [InjectAsSingleton(typeof(ITariffRoleRedisDbExecuter))]
    internal sealed class TariffRoleRedisDbExecutor : RedisDbExecuterBase, ITariffRoleRedisDbExecuter
    {
        public TariffRoleRedisDbExecutor(
            ISettingRepository settingRepository,
            IRedisDbExecutor redisDbExecutor,
            IAuditTracer auditTracer)
            : base(
                redisDbExecutor,
                settingRepository,
                settingRepository.Get("RedisCacheConnection"),
                (int)RedisCacheDbEnum.TariffRole,
                auditTracer)
        {
        }
    }
}