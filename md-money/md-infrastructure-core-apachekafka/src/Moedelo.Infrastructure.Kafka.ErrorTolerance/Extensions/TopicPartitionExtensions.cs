using Confluent.Kafka;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Extensions;

internal static class TopicPartitionExtensions
{
    internal static KafkaTopicPartition ToDomain(this TopicPartition topicPartition)
    {
        return new(topicPartition.Topic, topicPartition.Partition.Value);
    }

    internal static string ToShortString(this TopicPartition topicPartition)
    {
        return $@"{topicPartition.Topic}[{topicPartition.Partition.Value}]";
    }
}
