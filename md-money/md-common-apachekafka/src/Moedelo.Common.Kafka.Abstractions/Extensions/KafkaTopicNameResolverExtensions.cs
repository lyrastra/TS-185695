using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Extensions;

internal static class KafkaTopicNameResolverExtensions
{
    internal static KafkaTopicName GetKafkaTopicName(
        this IKafkaTopicNameResolver topicNameResolver,
        string rawTopicName)
    {
        return new KafkaTopicName(
            rawTopicName,
            topicNameResolver.GetTopicFullName(rawTopicName));
    }
}
