using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Extensions;

internal static class KafkaConsumerSettingsExtensions
{
    internal static KafkaConsumerConfig CreateConsumerConnectionSettings(
        this KafkaConsumerSettings settings,
        string brokerEndpoints,
        KafkaConsumerGroupId consumerGroupId,
        KafkaTopicName topicName)
    {
        return new KafkaConsumerConfig(
            brokerEndpoints,
            consumerGroupId,
            topicName)
        {
            ResetType = settings.ResetType,
            OptionalParams = new()
            {
                FetchWaitMaxMs = settings.FetchWaitMaxMs,
                FetchErrorBackoffMs = settings.FetchErrorBackoffMs,
                FetchMinBytes = settings.FetchMinBytes,
                FetchMaxBytes = settings.FetchMaxBytes,
                QueuedMinMessages = settings.QueuedMinMessages,
                SessionTimeoutMs = settings.SessionTimeoutMs,
                MaxPollIntervalMs = settings.MaxPollIntervalMs
            },
            ExtraOptions = settings.ExtraOptions,
            MaxCountOfIgnoringConsumeExceptionsInRow = settings.MaxCountOfIgnoringConsumeExceptionsInRow
        };
    }
}
