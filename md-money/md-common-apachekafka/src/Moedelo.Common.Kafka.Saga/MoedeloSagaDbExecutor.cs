using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.PostgreSqlDataAccess.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface;

namespace Moedelo.Common.Kafka.Saga
{
    [InjectAsSingleton(typeof(MoedeloSagaDbExecutor))]
    internal sealed class MoedeloSagaDbExecutor : MoedeloPostgreSqlDbExecutorBase
    {
        public MoedeloSagaDbExecutor(
            IPostgreSqlExecutor sqlDbExecutor,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer)
            : base(sqlDbExecutor, settingRepository.Get("MoedeloSagaDbConnectionString"), auditTracer)
        {
        }
    }
}