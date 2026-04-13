using Moedelo.InfrastructureV2.DataAccess.Extensions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

namespace Moedelo.InfrastructureV2.DataAccess
{
    [InjectAsSingleton(typeof(IMoedeloDbExecutor))]
    public class MoedeloDbExecutor : DbExecutor, IMoedeloDbExecutor
    {
        public MoedeloDbExecutor(
            ISettingRepository settingRepository,
            IAuditTracer auditTracer)
            : base(settingRepository.GetMoedeloConnectionString(), auditTracer)
        {
        }
    }
}