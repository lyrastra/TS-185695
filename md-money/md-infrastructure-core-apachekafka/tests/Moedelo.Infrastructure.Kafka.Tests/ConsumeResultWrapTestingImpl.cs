using Moedelo.Infrastructure.Kafka.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.Tests;

internal sealed class ConsumeResultWrapTestingImpl : IConsumeResultWrap
{
    public string? MessageKey { get; init; }
    public string? MessageValue { get; init; }
    public int Partition => TopicPartitionOffset.Partition;
    public long Offset => TopicPartitionOffset.Offset;
    public KafkaTopicPartitionOffset TopicPartitionOffset { get; init; }
}
