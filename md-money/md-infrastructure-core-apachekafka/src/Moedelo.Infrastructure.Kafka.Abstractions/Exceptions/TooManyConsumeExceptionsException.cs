using System;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Exceptions;

public class TooManyConsumeExceptionsException : MoedeloInfrastructureKafkaException
{
    public TooManyConsumeExceptionsException(
        int exceptionsCount,
        Exception lastConsumeException)
        : base(
            $"Слишком много исключений типа ConsumeException подряд. Последнее: {lastConsumeException.Message}",
            lastConsumeException)
    {
        this.ExceptionsCount = exceptionsCount;
    }

    /// <summary>
    /// Количество исключений ConsumeException, произошедших подряд (без единого удачного Consume) 
    /// </summary>
    public int ExceptionsCount { get; }
}
