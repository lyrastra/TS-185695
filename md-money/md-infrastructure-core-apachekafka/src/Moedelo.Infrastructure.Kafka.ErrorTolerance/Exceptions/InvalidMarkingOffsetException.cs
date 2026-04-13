using Moedelo.Infrastructure.Kafka.Abstractions.Exceptions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;

public class InvalidMarkingOffsetException : MoedeloInfrastructureKafkaException
{
    public InvalidMarkingOffsetException(KafkaTopicPartitionOffset offset, string reason)
        : base($"Попытка поставить отметку на смещение {offset.Topic}[{offset.Partition}]@{offset.Offset} вызывает ошибку. Причина: {reason}")
    {
    }
}
