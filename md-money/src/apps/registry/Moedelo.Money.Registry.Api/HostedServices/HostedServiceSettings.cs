using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using System;

namespace Moedelo.Money.Registry.Api.HostedServices
{
    internal static class HostedServiceSettings
    {
        public const string GroupId = "Moedelo.Money.Registry.Api";

        public const int ConsumerCount = 1;

        public const KafkaConsumerConfig.AutoOffsetResetType FallbackOffset =
            KafkaConsumerConfig.AutoOffsetResetType.Latest;

        public static ConsumerActionRetrySettings RetrySettings = new(2, TimeSpan.FromMinutes(1));
    }
}
