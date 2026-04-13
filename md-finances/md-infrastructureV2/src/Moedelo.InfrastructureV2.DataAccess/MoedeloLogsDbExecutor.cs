using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

namespace Moedelo.InfrastructureV2.DataAccess
{
    [Inject(InjectionType.Singleton)]
    public class MoedeloLogsDbExecutor : DbExecutor, IMoedeloLogsDbExecutor
    {
        public MoedeloLogsDbExecutor(
            ISettingRepository settingRepository,
            IAuditTracer auditTracer)
            : base(settingRepository.Get("LogsConnectionString"), auditTracer)
        {
        }
    }
}