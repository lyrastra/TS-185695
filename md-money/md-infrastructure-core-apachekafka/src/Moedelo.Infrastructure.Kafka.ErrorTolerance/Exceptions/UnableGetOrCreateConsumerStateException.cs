using System.Reflection;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;

public sealed class UnableGetOrCreateConsumerStateException : Exception
{
    private UnableGetOrCreateConsumerStateException(KafkaTopicPartition topicPartition, MemberInfo repositoryType)
    : base($"Репозиторий {repositoryType.Name} вернул null при запросе состояния для {topicPartition.Topic}@{topicPartition.Partition}")
    {
    }

    internal static void ThrowIfNull(IPartitionConsumingReadOnlyState? state, KafkaTopicPartition topicPartition, MemberInfo repositoryType)
    {
        if (state == null)
        {
            throw new UnableGetOrCreateConsumerStateException(topicPartition, repositoryType);
        }
    }
}