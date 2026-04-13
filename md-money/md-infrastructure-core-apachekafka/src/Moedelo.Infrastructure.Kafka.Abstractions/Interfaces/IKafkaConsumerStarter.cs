using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;

public interface IKafkaConsumerEventSource
{
    delegate Task ConsumerStarted(ConsumerStartedEvent @event, CancellationToken cancellation);
    delegate Task ConsumerStopped(ConsumerStoppedEvent @event, CancellationToken cancellation);
    delegate Task PartitionSetOnPause(ConsumerPartitionSetOnPauseEvent @event, CancellationToken cancellation);

    event ConsumerStarted ConsumerStartedEvent;
    event ConsumerStopped ConsumerStoppedEvent;
    event PartitionSetOnPause PartitionSetOnPauseEvent;
}

public interface IKafkaConsumerStarter : IKafkaConsumerEventSource
{
    Task ListenAsync<TMessage>(KafkaConsumerSettings<TMessage> consumerSettings, CancellationToken cancellationToken)
        where TMessage : KafkaMessageValueBase;
}