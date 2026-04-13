namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

public interface IPartitionConsumingStateFactory
{
    IPartitionConsumingState Create(string consumerGroupId, string topic, int partition, int maxOffsetMapDepth);
}
