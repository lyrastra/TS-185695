using System;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Exceptions;

public abstract class MoedeloInfrastructureKafkaException : Exception
{
    protected MoedeloInfrastructureKafkaException(string message)
        : base(message)
    {
    }

    protected MoedeloInfrastructureKafkaException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
