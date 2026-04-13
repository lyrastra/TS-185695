using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.PostgreSqlDataAccess;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

namespace Moedelo.InfrastructureV2.PostgreSqlDataAccess.OfficeExecutor
{
    [InjectAsSingleton]
    public class OfficePostgreSqlDbExecutor : PostgreSqlDbExecutor, IOfficePostgreSqlDbExecutor
    {
        public OfficePostgreSqlDbExecutor(
            ISettingRepository settingRepository,
            IAuditTracer auditTracer)
            : base(settingRepository.Get("OfficeConnectionString"), auditTracer)
        {
        }
    }
}
