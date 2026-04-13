using Confluent.Kafka;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Extensions;

namespace Moedelo.Infrastructure.Kafka;

public sealed class ConsumeResultWrapImpl : IConsumeResultWrap
{
    internal readonly ConsumeResult<string, string> consumeResult;
    
    public ConsumeResultWrapImpl(ConsumeResult<string, string> consumeResult)
    {
        this.consumeResult = consumeResult;
    }

    public string? MessageKey => consumeResult?.Message?.Key;
    public string? MessageValue => consumeResult?.Message?.Value;
    public int Partition => consumeResult.Partition.Value;
    public long Offset => consumeResult.Offset.Value;
    public KafkaTopicPartitionOffset TopicPartitionOffset => consumeResult.TopicPartitionOffset.ToDomain();
}
