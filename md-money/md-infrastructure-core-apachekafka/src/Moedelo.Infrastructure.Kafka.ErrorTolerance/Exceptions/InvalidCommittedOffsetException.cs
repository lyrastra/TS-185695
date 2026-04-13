using Moedelo.Infrastructure.Kafka.Abstractions.Exceptions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;

public class InvalidCommittedOffsetException : MoedeloInfrastructureKafkaException
{
    public InvalidCommittedOffsetException(KafkaTopicPartitionOffset offset, string reason)
        : base($"Попытка закоммитить смещение {offset.Topic}[{offset.Partition}]@{offset.Offset} вызывает ошибку. Причина: {reason}")
    {
    }
}