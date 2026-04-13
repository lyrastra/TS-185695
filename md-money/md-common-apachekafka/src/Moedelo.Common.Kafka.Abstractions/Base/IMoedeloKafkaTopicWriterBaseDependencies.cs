using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces.ProducingRetry;

namespace Moedelo.Common.Kafka.Abstractions.Base
{
    public interface IMoedeloKafkaTopicWriterBaseDependencies
    {
        IKafkaProducer Producer { get; }
        ISettingRepository SettingRepository { get; }
        IExecutionInfoContextAccessor ContextAccessor { get; }
        IKafkaTopicNameResolver TopicNameResolver { get; }
        IAuditScopeManager AuditScopeManager { get; }
        IKafkaProducingRetryPolicy ProducingRetryPolicy { get; }
        IKafkaProducingRetryPolicy ProducingNoRetryPolicy { get; }
    }
}
