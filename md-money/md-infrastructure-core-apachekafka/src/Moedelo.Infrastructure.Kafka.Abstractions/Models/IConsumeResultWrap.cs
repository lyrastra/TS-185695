namespace Moedelo.Infrastructure.Kafka.Abstractions.Models;

public interface IConsumeResultWrap
{
    public string? MessageKey { get; }
    
    public string? MessageValue { get; }

    public int Partition { get; }

    public long Offset { get; }

    public KafkaTopicPartitionOffset TopicPartitionOffset { get; }
}
