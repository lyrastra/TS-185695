using System;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Models;

[Obsolete("Это класс-прокладка для сохранения обратной совместимости, он скоро будет удалён. Замени на использование KafkaConsumerConfig.AutoOffsetResetType.*")]
public static class ConsumerConnectionSettings
{
    public static class AutoOffsetResetType
    {
        public const KafkaConsumerConfig.AutoOffsetResetType Earliest = KafkaConsumerConfig.AutoOffsetResetType.Earliest;
        public const KafkaConsumerConfig.AutoOffsetResetType Latest = KafkaConsumerConfig.AutoOffsetResetType.Latest;
    }
}
