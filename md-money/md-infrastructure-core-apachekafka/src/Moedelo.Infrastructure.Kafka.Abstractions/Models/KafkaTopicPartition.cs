namespace Moedelo.Infrastructure.Kafka.Abstractions.Models;

public readonly struct KafkaTopicPartition
{
    public KafkaTopicPartition(string topic, int partition)
    {
        Topic = topic;
        Partition = partition;
    }

    public string Topic { get; }
    public int Partition { get; }

    public static KafkaTopicPartition Unknown => new KafkaTopicPartition("НеизвестныйТопик", 0);
}
