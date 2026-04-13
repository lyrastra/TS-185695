using System;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor
{
    /// <summary>
    /// DTO состояния Kafka consumer
    /// </summary>
    public class KafkaConsumerStateDto
    {
        /// <summary>
        /// Consumer group ID
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// Kafka topic
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// Client ID / Service ID
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// Member ID consumer'а в группе
        /// </summary>
        public string ConsumerGuid { get; set; }

        /// <summary>
        /// Состояние consumer group (Stable, Empty, Dead, и т.д.)
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Список назначенных партиций
        /// </summary>
        public List<int> AssignedPartitions { get; set; } = new List<int>();

        /// <summary>
        /// Суммарный лаг по всем партициям
        /// </summary>
        public long TotalLag { get; set; }

        /// <summary>
        /// Лаг по каждой партиции (partition -> lag)
        /// </summary>
        public Dictionary<string, long> PartitionLags { get; set; } = new Dictionary<string, long>();

        /// <summary>
        /// Committed offset по каждой партиции
        /// </summary>
        public Dictionary<string, long> CommittedOffsets { get; set; } = new Dictionary<string, long>();

        /// <summary>
        /// End offset (high watermark) по каждой партиции
        /// </summary>
        public Dictionary<string, long> EndOffsets { get; set; } = new Dictionary<string, long>();

        /// <summary>
        /// Есть ли проблема с лагом (лаг растёт или слишком большой)
        /// </summary>
        public bool HasLagProblem { get; set; }

        /// <summary>
        /// Есть ли партиции, поставленные на паузу из-за ошибок
        /// </summary>
        public bool HasPausedPartitions { get; set; }

        /// <summary>
        /// Количество партиций на паузе
        /// </summary>
        public int PausedPartitionsCount { get; set; }

        /// <summary>
        /// Максимальное время ожидания сообщения (в секундах), если известно
        /// </summary>
        public double? MaxLagTimeSeconds { get; set; }

        /// <summary>
        /// Timestamp самого старого необработанного сообщения (UTC)
        /// </summary>
        public DateTime? OldestUnprocessedMessageTime { get; set; }

        /// <summary>
        /// Есть ли секции, окончательно остановленные из-за превышения допустимого времени
        /// без коммита (ошибка "обработка секции окончательно остановлена").
        /// </summary>
        public bool HasFinallyPausedPartitions { get; set; }

        /// <summary>
        /// Количество секций, окончательно остановленных из-за превышения допустимого
        /// времени без коммита.
        /// </summary>
        public int FinallyPausedPartitionsCount { get; set; }
    }
}
