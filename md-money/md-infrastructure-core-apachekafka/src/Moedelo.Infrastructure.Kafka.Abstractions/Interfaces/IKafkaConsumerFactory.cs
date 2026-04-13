using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;

public interface IKafkaConsumerFactory
{
    Task<IKafkaConsumer> CreateAsync(KafkaConsumerConfig config,
        IKafkaConsumerFactorySettings kafkaConsumerFactorySettings,
        ILogger logger);
}
