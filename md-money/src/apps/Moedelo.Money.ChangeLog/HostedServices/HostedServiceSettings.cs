using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using System;

namespace Moedelo.Money.ChangeLog.HostedServices
{
    internal static class HostedServiceSettings
    {
        public const int ConsumerCount = 1;

        public const KafkaConsumerConfig.AutoOffsetResetType FallbackOffset =
            KafkaConsumerConfig.AutoOffsetResetType.Latest;

        public static ConsumerActionRetrySettings RetrySettings =
            new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1));
    }
}
