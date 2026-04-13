using Moedelo.Common.Audit.Configuration.Interfaces;
using Moedelo.Common.Audit.EmptyMock;
using Moedelo.Common.Audit.Extensions;
using Moedelo.Common.Redis.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Redis.Abstractions.Interfaces;

namespace Moedelo.Common.Audit.Configuration
{
    [InjectAsSingleton(typeof(IAuditRedisDbExecuter))]
    internal sealed class AuditRedisDbExecutor : MoedeloRedisDbExecutorBase, IAuditRedisDbExecuter
    {
        public AuditRedisDbExecutor(
            IRedisDbExecuter redisExecutor,
            ISettingRepository settingRepository)
            : base(
                redisExecutor,
                settingRepository,
                settingRepository.GetRedisConnectionSetting(),
                settingRepository.GetAuditRedisDbNumber(),
                EmptyAuditTracer.Instance)
        {
        }
    }
}