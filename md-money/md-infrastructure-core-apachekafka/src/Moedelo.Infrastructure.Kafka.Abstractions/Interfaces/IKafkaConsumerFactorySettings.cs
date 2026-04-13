using System;
using Microsoft.Extensions.Logging;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Interfaces
{
    /// <summary>
    /// Настройки фабрики консьюмеров
    /// ВНИМАНИЕ: Moedelo.Infrastructure.Kafka не содержит реализацию этого интерфейса
    /// </summary>
    public interface IKafkaConsumerFactorySettings
    {
        LogLevel CommitmentLogLevel { get; }
        LogLevel ConsumingLogLevel { get; }
        LogLevel ConsumedLogLevel { get; }
        /// <summary>
        /// Пауза, которую надо выдержать на старте первого консьюмера
        /// </summary>
        TimeSpan PauseBeforeFirstConsumerStart { get; }
    }
}