using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;

namespace Moedelo.Common.Kafka;

[InjectAsSingleton(typeof(IMoedeloKafkaTopicReaderBaseDependencies))]
internal sealed class MoedeloKafkaTopicReaderBaseDependencies : IMoedeloKafkaTopicReaderBaseDependencies
{
    public MoedeloKafkaTopicReaderBaseDependencies(
        IKafkaConsumerStarter consumerStarter,
        ISettingRepository settingRepository,
        IExecutionInfoContextInitializer contextInitializer,
        IExecutionInfoContextAccessor contextAccessor,
        IKafkaTopicNameResolver topicNameResolver,
        IKafkaConsumerBalancer kafkaConsumerBalancer,
        IAuditTracer auditTracer)
    {
        ConsumerStarter = consumerStarter;
        SettingRepository = settingRepository;
        KafkaConsumerBalancer = kafkaConsumerBalancer;
        ContextInitializer = contextInitializer;
        ContextAccessor = contextAccessor;
        TopicNameResolver = topicNameResolver;
        AuditTracer = auditTracer;
    }

    public IKafkaConsumerStarter ConsumerStarter { get; }
    public ISettingRepository SettingRepository { get; }
    public IKafkaConsumerBalancer KafkaConsumerBalancer { get; }
    public IExecutionInfoContextInitializer ContextInitializer { get; }
    public IExecutionInfoContextAccessor ContextAccessor { get; }
    public IKafkaTopicNameResolver TopicNameResolver { get; }
    public IAuditTracer AuditTracer { get; }
}