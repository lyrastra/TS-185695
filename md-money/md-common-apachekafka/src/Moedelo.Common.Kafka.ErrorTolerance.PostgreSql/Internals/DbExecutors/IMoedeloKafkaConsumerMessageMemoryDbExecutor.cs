using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.PostgreSqlDataAccess.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface;

namespace Moedelo.Common.Kafka.ErrorTolerance.PostgreSql.Internals.DbExecutors;

public interface IMoedeloKafkaConsumerMessageMemoryDbExecutor : IMoedeloPostgreSqlDbExecutorBase
{
    
}

[InjectAsSingleton(typeof(IMoedeloKafkaConsumerMessageMemoryDbExecutor))]
internal sealed class MoedeloKafkaConsumerMessageMemoryDbExecutor : MoedeloPostgreSqlDbExecutorBase,
    IMoedeloKafkaConsumerMessageMemoryDbExecutor
{
    public MoedeloKafkaConsumerMessageMemoryDbExecutor(
        IPostgreSqlExecutor sqlDbExecutor,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer) : base(
            sqlDbExecutor,
        settingRepository.Get("KafkaConsumerMessageMemoryConnectionString").ThrowExceptionIfNull(true),
            auditTracer)
    {
    }
}
