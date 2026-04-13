using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Models;

/// <summary>
/// Контекст сервиса для автобалансировки консьюмеров Kafka
/// </summary>
public sealed class ServiceKafkaBalanceContext
{
    internal ServiceKafkaBalanceContext(
        string serviceId,
        KafkaConsumerConfig kafkaConsumerSettings,
        string consulKeyPath)
    {
        ConsulKeyPath = consulKeyPath;
        KafkaConsumerSettings = kafkaConsumerSettings;
        ServiceId = serviceId;
    }

    /// <summary>
    /// Идентификатор сервиса в Service Delivery
    /// </summary>
    public string ServiceId { get; }

    /// <summary>
    /// Путь до ключа в consul
    /// </summary>
    internal string ConsulKeyPath { get; }
    
    internal KafkaConsumerConfig KafkaConsumerSettings { get; }
}
