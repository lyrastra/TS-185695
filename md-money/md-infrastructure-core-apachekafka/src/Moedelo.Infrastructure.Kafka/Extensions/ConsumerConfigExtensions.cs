using System;
using Confluent.Kafka;

namespace Moedelo.Infrastructure.Kafka.Extensions;

internal static class ConsumerConfigExtensions
{
    internal static bool IsMaxPollIntervalExceeded(
        this ConsumerConfig consumerConfig, TimeSpan duration)
    {
        return consumerConfig?.SessionTimeoutMs.HasValue == true // не уверен, что это условие необходимо - оставил, поскольку оно уже было
               && duration.TotalMilliseconds > consumerConfig.MaxPollIntervalMs;
    }
}
