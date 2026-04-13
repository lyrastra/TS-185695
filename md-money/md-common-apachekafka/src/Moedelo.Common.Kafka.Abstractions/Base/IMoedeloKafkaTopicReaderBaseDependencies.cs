using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;

namespace Moedelo.Common.Kafka.Abstractions.Base
{
    public interface IMoedeloKafkaTopicReaderBaseDependencies
    {
        public IKafkaConsumerStarter ConsumerStarter { get; }
        public ISettingRepository SettingRepository { get; }
        public IKafkaConsumerBalancer KafkaConsumerBalancer { get; }
        public IExecutionInfoContextInitializer ContextInitializer { get; }
        public IExecutionInfoContextAccessor ContextAccessor { get; }
        public IKafkaTopicNameResolver TopicNameResolver { get; }
        public IAuditTracer AuditTracer { get; }
    }
}
