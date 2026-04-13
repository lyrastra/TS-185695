using Moedelo.Common.AccessRules.Extensions;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Redis.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Redis.Abstractions.Interfaces;

namespace Moedelo.Common.AccessRules.Infrastructure
{
    [InjectAsSingleton(typeof(ITariffsAndRolesRedisDbExecutor))]
    internal sealed class TariffsAndRolesRedisDbExecutor : MoedeloRedisDbExecutorBase, ITariffsAndRolesRedisDbExecutor
    {
        public TariffsAndRolesRedisDbExecutor(
            IRedisDbExecuter redisExecutor,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer)
            : base(redisExecutor,
                settingRepository,
                settingRepository.GetRedisCacheConnectionSetting(),
                settingRepository.GetTariffsAndRolesRedisDbNumber(), 
                auditTracer)
        {
        }
    }
}
