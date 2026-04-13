using Confluent.Kafka;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.Extensions
{
    public static class TopicPartitionOffsetExtensions
    {
        public static string ToShortString(this TopicPartitionOffset tpo)
        {
            return $"{tpo.Topic}[{tpo.Partition}]@{tpo.Offset}";
        }
        
        public static string ToShortString(this KafkaTopicPartitionOffset tpo)
        {
            return $"{tpo.Topic}[{tpo.Partition}]@{tpo.Offset}";
        }

        public static KafkaTopicPartitionOffset ToDomain(this TopicPartitionOffset? tpo)
        {
            if (tpo == null)
            {
                return KafkaTopicPartitionOffset.Unknown; 
            }

            return new KafkaTopicPartitionOffset
            {
                Topic = tpo.Topic,
                Partition = tpo.Partition.Value,
                Offset = tpo.Offset.Value
            };
        }
    }
}
