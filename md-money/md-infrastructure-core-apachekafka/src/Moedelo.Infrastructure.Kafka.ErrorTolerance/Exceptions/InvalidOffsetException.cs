using Moedelo.Infrastructure.Kafka.Abstractions.Exceptions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;

public sealed class InvalidOffsetException : MoedeloInfrastructureKafkaException
{
    public InvalidOffsetException(string message) : base(message)
    {
    }
}
