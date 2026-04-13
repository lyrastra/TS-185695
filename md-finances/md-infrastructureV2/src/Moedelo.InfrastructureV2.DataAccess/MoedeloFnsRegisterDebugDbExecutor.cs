using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

namespace Moedelo.InfrastructureV2.DataAccess
{
    [InjectAsSingleton]
    public class MoedeloFnsRegisterDebugDbExecutor : DbExecutor, IMoedeloFnsRegisterDebugDbExecutor
    {
        public MoedeloFnsRegisterDebugDbExecutor(
            ISettingRepository settingRepository,
            IAuditTracer auditTracer)
            : base(settingRepository.Get("FnsRegisterDebugConnectionString"), auditTracer)
        {
        }
    }
}