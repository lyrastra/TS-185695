using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

namespace Moedelo.InfrastructureV2.DataAccess;

[InjectAsSingleton(typeof(IMoedeloReportsReadonlyDbExecutor))]
// ReSharper disable once UnusedType.Global
internal sealed class MoedeloReportsReadonlyDbExecutor : DbExecutor, IMoedeloReportsReadonlyDbExecutor
{
    public MoedeloReportsReadonlyDbExecutor(
        ISettingRepository settingRepository,
        IAuditTracer auditTracer)
        : base(settingRepository.GetRequired("ReportsReadOnlyConnectionString"),
            auditTracer)
    {
    }
}
