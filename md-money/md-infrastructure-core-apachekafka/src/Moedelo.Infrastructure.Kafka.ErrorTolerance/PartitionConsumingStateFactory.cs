using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

[InjectAsSingleton(typeof(IPartitionConsumingStateFactory))]
internal sealed class PartitionConsumingStateFactory : IPartitionConsumingStateFactory
{
    public IPartitionConsumingState Create(string consumerGroupId, string topic, int partition, int maxOffsetMapDepth)
    {
        return new PartitionConsumingState(consumerGroupId, topic, partition, maxOffsetMapDepth);
    }
}
