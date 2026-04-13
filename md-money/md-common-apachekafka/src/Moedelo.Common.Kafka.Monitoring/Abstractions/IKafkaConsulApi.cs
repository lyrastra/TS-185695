using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Monitoring.Abstractions;

public interface IKafkaConsulApi
{
    ValueTask NotifyAboutConsumerStartedAsync(
        string consulConsumersDirectory,
        ConsumerStartedEvent @event,
        CancellationToken cancellation);

    ValueTask NotifyAboutConsumerStoppedAsync(
        string consulConsumersDirectory,
        ConsumerStoppedEvent @event,
        CancellationToken cancellation);

    ValueTask NotifyAboutConsumerSetPartitionOnPauseAsync(
        string consulConsumersDirectory,
        ConsumerPartitionSetOnPauseEvent @event,
        CancellationToken cancellation);
}
