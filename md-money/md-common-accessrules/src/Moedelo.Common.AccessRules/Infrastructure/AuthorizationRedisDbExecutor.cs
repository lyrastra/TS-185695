using Moedelo.Common.AccessRules.Extensions;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Redis.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Redis.Abstractions.Interfaces;

namespace Moedelo.Common.AccessRules.Infrastructure
{
    [InjectAsSingleton(typeof(IAuthorizationRedisDbExecutor))]
    internal sealed class AuthorizationRedisDbExecutor : MoedeloRedisDbExecutorBase, IAuthorizationRedisDbExecutor
    {
        public AuthorizationRedisDbExecutor(
            IRedisDbExecuter redisDbExecutor,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer)
            : base(redisDbExecutor,
                settingRepository,
                settingRepository.GetRedisCacheConnectionSetting(),
                settingRepository.GetAuthorizationRedisDbNumber(), 
                auditTracer)
        {
        }
    }
}
