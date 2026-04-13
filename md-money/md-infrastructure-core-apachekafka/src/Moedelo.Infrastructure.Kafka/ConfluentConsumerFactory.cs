using Confluent.Kafka;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions;
using Moedelo.Infrastructure.Kafka.Extensions;

namespace Moedelo.Infrastructure.Kafka;

[InjectAsSingleton(typeof(IConfluentConsumerFactory))]
internal sealed class ConfluentConsumerFactory : IConfluentConsumerFactory
{
    public IConsumer<string, string> Create(ConsumerConfig consumerConfig, ConfluentConsumerEventHandlers handlers)
    {
        return new ConsumerBuilder<string, string>(consumerConfig)
            .SetHandlers(handlers)
            .Build();
    }
}
