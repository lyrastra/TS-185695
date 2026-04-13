namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

public interface IKafkaConsumerMessageMemoryRepository
{
    Task SaveAsync(IPartitionConsumingReadOnlyState partitionConsumingState, CancellationToken cancellationToken);
    Task<IPartitionConsumingReadOnlyState> GetOrCreateAsync(string consumerGroupId, string topic, int partition,
        CancellationToken cancellationToken);
}