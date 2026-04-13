using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Exceptions;

public class UnexpectedNullConsumeResultException : MoedeloInfrastructureKafkaException
{
    public KafkaConsumerConfig ConsumerConfig { get; } 
    
    public UnexpectedNullConsumeResultException(KafkaConsumerConfig config)
        : base($"Unexpected null consumeResult. Topic: {config.TopicName.Raw}, Group: {config.GroupId.Raw}")
    {
        ConsumerConfig = config;
    }
}
