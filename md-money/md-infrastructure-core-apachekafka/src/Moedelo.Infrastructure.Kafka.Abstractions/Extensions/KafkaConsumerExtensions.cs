using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Extensions;

internal static class KafkaConsumerExtensions
{
    internal static IKafkaConsumer SubscribeOrDispose(this IKafkaConsumer consumer, string topicName)
    {
        try
        {
            consumer.Subscribe(topicName);

            return consumer;
        }
        catch
        {
            consumer.Dispose();
            throw;
        }
    }
}
