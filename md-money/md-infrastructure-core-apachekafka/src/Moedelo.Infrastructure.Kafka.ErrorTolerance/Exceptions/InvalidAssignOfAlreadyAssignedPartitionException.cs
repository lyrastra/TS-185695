using Moedelo.Infrastructure.Kafka.Abstractions.Exceptions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;

public class InvalidAssignOfAlreadyAssignedPartitionException : MoedeloInfrastructureKafkaException
{
    public InvalidAssignOfAlreadyAssignedPartitionException(string topic, int partition)
        : base($"Попытка назвачения консьюмеру секцией топика {topic}[{partition}], которая уже была ему назначена")
    {
    }
}