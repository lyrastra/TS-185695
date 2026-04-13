using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;

public sealed class InvalidConsumingStateException : Exception
{
    private InvalidConsumingStateException(IPartitionConsumingReadOnlyState state, KafkaTopicPartition expected)
        : base($"Несоответствие загруженного состояния запрошенным параметрам: {expected.Topic}[{expected.Partition}] != {state.Topic}[{state.Partition}]")
    {
    }

    internal static void ThrowIfNotMatched(IPartitionConsumingReadOnlyState state, KafkaTopicPartition partition)
    {
        if (state.Topic != partition.Topic || state.Partition != partition.Partition)
        {
            throw new InvalidConsumingStateException(state, partition);
        }
    }
}
