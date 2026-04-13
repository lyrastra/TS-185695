namespace Moedelo.Infrastructure.Kafka.Abstractions.Exceptions;

/// <summary>
/// Фатальная ошибка при попытка получения сообщения консьюмером
/// </summary>
public class ConsumerFatalException: MoedeloInfrastructureKafkaException
{
    public ConsumerFatalException() : base("Неизвестная фатальная ошибка")
    {
        this.ErrorName = "unknown";
        this.ErrorCode = -1;
    }

    public ConsumerFatalException(string reason, string errorName, int errorCode)
        : base($"Фатальная ошибка получения сообщения консьюмером: {reason}")
    {
        this.ErrorName = errorName;
        this.ErrorCode = errorCode;
    }

    public int ErrorCode { get; }

    public string ErrorName { get; }
}