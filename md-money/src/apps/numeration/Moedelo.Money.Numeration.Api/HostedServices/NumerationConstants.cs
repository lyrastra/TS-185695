using System;
using Moedelo.Common.Kafka.Abstractions.Settings;

namespace Moedelo.Money.Numeration.Api.HostedServices
{
    public class NumerationConstants
    {
        internal const string GroupId = "Moedelo.Money.Numeration.Api";

        internal const int ConsumerCount = 1;
    
        public static readonly ConsumerActionRetrySettings RetrySettings = new(3, TimeSpan.FromMinutes(2));
    }
}