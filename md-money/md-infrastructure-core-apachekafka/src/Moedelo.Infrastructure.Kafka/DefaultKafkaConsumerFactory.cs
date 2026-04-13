using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Implementations;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka;

[InjectAsSingleton(typeof(IDefaultKafkaConsumerFactory))]
internal sealed class DefaultKafkaConsumerFactory: AbstractKafkaConsumerFactory, IDefaultKafkaConsumerFactory
{
    private readonly IConfluentConsumerFactory confluentConsumerFactory;

    public DefaultKafkaConsumerFactory(IConfluentConsumerFactory confluentConsumerFactory)
    {
        this.confluentConsumerFactory = confluentConsumerFactory;
    }

    protected override IKafkaConsumer Create(
        KafkaConsumerConfig config,
        ILogger logger)
    {
        return new KafkaConsumerImpl(config, confluentConsumerFactory, logger);
    }
}
