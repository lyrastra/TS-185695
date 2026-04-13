using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Redis.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Redis.Abstractions.Interfaces;

namespace Moedelo.Money.Business.Operations.TaxationSystemChangingSync
{
    [InjectAsSingleton(typeof(TaxationSystemChangingSyncRedisExecuter))]
    public class TaxationSystemChangingSyncRedisExecuter: MoedeloRedisDbExecutorBase
    {
        public static string TaxationSystemChangingSyncKey = "TaxationSystemChangingSync";
        
        public TaxationSystemChangingSyncRedisExecuter(
            IRedisDbExecuter redisDbExecutor,
            ISettingRepository settingRepository, 
            IAuditTracer auditTracer) 
            : base(redisDbExecutor,
                settingRepository,
                settingRepository.Get("RedisConnection"),
                settingRepository.Get("MoneyTaxationSystemChangingSyncRedisDatabase"), 
                auditTracer)
        {
        }
    }
}