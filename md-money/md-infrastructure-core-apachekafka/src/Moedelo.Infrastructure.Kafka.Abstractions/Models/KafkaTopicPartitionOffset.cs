namespace Moedelo.Infrastructure.Kafka.Abstractions.Models;

public struct KafkaTopicPartitionOffset
{
    public KafkaTopicPartitionOffset()
    {
        Topic = "НеизвестныйТопик";
        Partition = default;
        Offset = default;
    }

    public KafkaTopicPartitionOffset(string topic, int partition, long offset)
    {
        Topic = topic;
        Partition = partition;
        Offset = offset;
    }

    public string Topic { get; set; }
    public int Partition { get; set; }
    public long Offset { get; set; }

    public static KafkaTopicPartitionOffset Unknown => new ();
}
