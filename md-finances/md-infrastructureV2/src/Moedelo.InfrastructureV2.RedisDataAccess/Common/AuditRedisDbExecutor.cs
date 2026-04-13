using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.RedisDataAccess.Common.AuditEmptyMock;
using Moedelo.InfrastructureV2.RedisDataAccess.Models;
using IRedisDbExecutor = Moedelo.Infrastructure.Redis.Abstractions.Interfaces.IRedisDbExecuter;

namespace Moedelo.InfrastructureV2.RedisDataAccess.Common
{
    [InjectAsSingleton(typeof(IAuditRedisDbExecuter))]
    internal sealed class AuditRedisDbExecutor : RedisDbExecuterBase, IAuditRedisDbExecuter
    {
        public AuditRedisDbExecutor(
            ISettingRepository settingRepository,
            IRedisDbExecutor baseExecutor)
            : base(
                baseExecutor,
                settingRepository,
                settingRepository.Get("RedisConnection"),
                (int) RedisDbEnum.AuditDb,
                EmptyAuditTracer.Instance)
        {
        }
    }
}