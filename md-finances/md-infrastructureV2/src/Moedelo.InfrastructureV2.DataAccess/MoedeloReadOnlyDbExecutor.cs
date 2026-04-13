using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

namespace Moedelo.InfrastructureV2.DataAccess;

[InjectAsSingleton(typeof(IMoedeloReadOnlyDbExecutor))]
// ReSharper disable once UnusedType.Global
internal sealed class MoedeloReadOnlyDbExecutor : DbExecutor, IMoedeloReadOnlyDbExecutor
{
    public MoedeloReadOnlyDbExecutor(
        ISettingRepository settingRepository,
        IAuditTracer auditTracer)
        : base(settingRepository.GetRequired("ReadOnlyConnectionString"), auditTracer)
    {
    }
}