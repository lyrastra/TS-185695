using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka;

public interface IConsumerObserverCollection
{
    /// <summary>
    ///  Количество запущенных консьюмеров
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Настройки запуска консьюмера
    /// </summary>
    public KafkaConsumerConfig Settings { get; }
}
