namespace Moedelo.Common.Kafka.Abstractions;

public struct ProducingSettings
{
    /// <summary>
    /// Не делать попыток повторной публикации сообщения, если первая попытка закончилась неудачно
    /// </summary>
    public bool NoRetryIfProducingFailed { get; set; }

    /// <summary>
    /// настройки по умолчанию
    /// </summary>
    public static ProducingSettings Default => new();
}
