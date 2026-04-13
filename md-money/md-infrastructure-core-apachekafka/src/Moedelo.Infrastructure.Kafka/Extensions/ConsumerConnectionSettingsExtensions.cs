using System;
using Confluent.Kafka;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.Extensions;

public static class ConsumerConnectionSettingsExtensions
{
    private static AutoOffsetReset GetAutoOffsetReset(this KafkaConsumerConfig config)
    {
        switch (config.ResetType)
        {
            case KafkaConsumerConfig.AutoOffsetResetType.Latest:
                return AutoOffsetReset.Latest;
            case KafkaConsumerConfig.AutoOffsetResetType.Earliest:
                return AutoOffsetReset.Earliest;
            default:
                throw new ArgumentOutOfRangeException(
                    nameof(KafkaConsumerConfig.ResetType),
                    config.ResetType, 
                    "Данное значение не поддерживается");
        }
    }

    public static ConsumerConfig GetConsumerConfig(this KafkaConsumerConfig config)
    {
        return new ConsumerConfig
            {
                BootstrapServers = config.BrokerEndpoints,
                GroupId = config.GroupId.IdInKafka,
                AutoOffsetReset = config.GetAutoOffsetReset(),
                EnableAutoCommit = false,
                ApiVersionRequest = true,
                SocketKeepaliveEnable = true,
                StatisticsIntervalMs = 0
            }
            .ApplyOptionalParams(config.OptionalParams);
    }

    private static ConsumerConfig ApplyOptionalParams(this ConsumerConfig rawConfig,
        KafkaConsumerConfigOptionalParams config)
    {
        if (config.FetchWaitMaxMs.HasValue)
        {
            rawConfig.FetchWaitMaxMs = config.FetchWaitMaxMs;
        }

        if (config.FetchErrorBackoffMs.HasValue)
        {
            rawConfig.FetchErrorBackoffMs = config.FetchErrorBackoffMs;
        }

        if (config.FetchMinBytes.HasValue)
        {
            rawConfig.FetchMinBytes = config.FetchMinBytes;
        }

        if (config.FetchMaxBytes.HasValue)
        {
            rawConfig.FetchMaxBytes = config.FetchMaxBytes;
        }

        if (config.QueuedMinMessages.HasValue)
        {
            rawConfig.QueuedMinMessages = config.QueuedMinMessages;
        }

        if (config.MaxPollIntervalMs.HasValue)
        {
            rawConfig.MaxPollIntervalMs = config.MaxPollIntervalMs;
        }

        if (config.SessionTimeoutMs.HasValue)
        {
            const int minSessionTimeoutMs = 6000;
            const int maxSessionTimeoutMs = 1800000;

            rawConfig.SessionTimeoutMs = Math.Clamp(
                config.SessionTimeoutMs.Value,
                minSessionTimeoutMs,
                maxSessionTimeoutMs);
        }

        return rawConfig;
    }
}
