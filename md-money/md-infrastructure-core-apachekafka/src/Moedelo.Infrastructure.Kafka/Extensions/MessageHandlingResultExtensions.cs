using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.Extensions;

internal static class MessageHandlingResultExtensions
{
    internal static ConsumerPartitionSetOnPauseEvent ToPartitionSetOnPauseEvent(
        this MessageHandlingResultBase handlingResult,
        string consumersGroupId,
        string topicName,
        string kafkaConsumerGuid)
    {
        var consumeResult = handlingResult.ConsumeResult;

        return new ConsumerPartitionSetOnPauseEvent
        {
            GroupId = consumersGroupId,
            ConsumerGuid = kafkaConsumerGuid,
            Topic = topicName,
            Partition = consumeResult.Partition,
            Offset = consumeResult.Offset,
            Exception = handlingResult.Exception
        };
    }
}
