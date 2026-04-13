using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests.Models;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests.Extensions;

internal static class ConsumingStateMemoryExtensions
{
    /// <summary>
    /// Консьюмеру назначена секция топика
    /// </summary>
    /// <param name="memory"></param>
    /// <param name="partition">информация о секции</param>
    internal static void Assigned(this IConsumingStateMemory memory, KafkaTopicPartition partition)
    {
        memory.Assigned(new PartitionConsumingReadOnlyState(
            memory.ConsumerGroupId,
            partition.Topic,
            partition.Partition,
            default,
            default,
            0,
            Array.Empty<byte>()));
    }
}
