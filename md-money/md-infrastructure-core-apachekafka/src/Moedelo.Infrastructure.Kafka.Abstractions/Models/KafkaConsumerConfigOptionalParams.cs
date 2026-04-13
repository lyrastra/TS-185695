namespace Moedelo.Infrastructure.Kafka.Abstractions.Models;

public sealed class KafkaConsumerConfigOptionalParams
{
    public int? FetchWaitMaxMs { get; set; }

    public int? FetchErrorBackoffMs { get; set; }

    public int? FetchMinBytes { get; set; }
        
    public int? FetchMaxBytes { get; set; }

    public int? QueuedMinMessages { get; set; }

    public int? SessionTimeoutMs { get; set; }

    /// <summary>
    /// Максимальное време между соседними вызовами Consume
    /// По сути, это максимальное время на обработку одного сообщения приложением
    /// </summary>
    public int? MaxPollIntervalMs { get; set; }
}
