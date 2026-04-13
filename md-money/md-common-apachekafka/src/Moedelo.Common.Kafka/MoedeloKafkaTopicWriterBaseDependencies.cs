using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Retries;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces.ProducingRetry;

namespace Moedelo.Common.Kafka;

[InjectAsSingleton(typeof(IMoedeloKafkaTopicWriterBaseDependencies))]
internal sealed class MoedeloKafkaTopicWriterBaseDependencies : IMoedeloKafkaTopicWriterBaseDependencies
{
    public MoedeloKafkaTopicWriterBaseDependencies(
        IKafkaProducer producer,
        ISettingRepository settingRepository,
        IExecutionInfoContextAccessor contextAccessor,
        IKafkaTopicNameResolver topicNameResolver,
        IAuditScopeManager auditScopeManager,
        IKafkaProducingRetryPolicy producingRetryPolicy)
    {
        Producer = producer;
        SettingRepository = settingRepository;
        ContextAccessor = contextAccessor;
        TopicNameResolver = topicNameResolver;
        AuditScopeManager = auditScopeManager;
        ProducingRetryPolicy = producingRetryPolicy;
        ProducingNoRetryPolicy = EmptyKafkaProducingRetryPolicy.Instance;
    }

    public IKafkaProducer Producer { get; }
    public ISettingRepository SettingRepository { get; }
    public IExecutionInfoContextAccessor ContextAccessor { get; }
    public IKafkaTopicNameResolver TopicNameResolver { get; }
    public IAuditScopeManager AuditScopeManager { get; }
    public IKafkaProducingRetryPolicy ProducingRetryPolicy { get; }
    public IKafkaProducingRetryPolicy ProducingNoRetryPolicy { get; }
}