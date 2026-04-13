using Moedelo.Infrastructure.Kafka.Abstractions.Exceptions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;

public class InvalidOperationOnNonAssignedPartitionException : MoedeloInfrastructureKafkaException
{
    public InvalidOperationOnNonAssignedPartitionException(string topic, int partition)
        : base($"Попытка выполнения операции над секцией топика {topic}[{partition}], которая ещё не была назначена консьюмеру")
    {
    }
}