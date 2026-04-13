using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.RedisDataAccess.Models;
using IRedisDbExecutor = Moedelo.Infrastructure.Redis.Abstractions.Interfaces.IRedisDbExecuter;

namespace Moedelo.InfrastructureV2.RedisDataAccess.Common
{
    [InjectAsSingleton(typeof(ITokenRedisDbExecuter))]
    internal sealed class TokenRedisDbExecutor : RedisDbExecuterBase, ITokenRedisDbExecuter
    {
        public TokenRedisDbExecutor(
            ISettingRepository settingRepository,
            IRedisDbExecutor redisExecutor,
            IAuditTracer auditTracer)
            : base(
                redisExecutor,
                settingRepository,
                settingRepository.Get("RedisConnection"),
                (int)RedisDbEnum.TokenDb,
                auditTracer)
        {
        }
    }
}