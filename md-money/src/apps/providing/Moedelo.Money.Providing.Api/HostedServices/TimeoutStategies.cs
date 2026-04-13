using System;

namespace Moedelo.Money.Providing.Api.HostedServices
{
    static class TimeoutStategies
    {
        /// <summary>
        /// Стратегия экспоненциального изменения таймаута
        /// Увелечение в степени retryCount по основанию 3
        /// now -> 3m -> 9m -> 27m ~= 39m
        /// </summary>
        public static Func<TimeSpan, int, TimeSpan> ExponentStategy =>
            (timeout, retryCount) =>
            {
                var exponent = Math.Pow(3, retryCount);
                var seconds = exponent * timeout.TotalSeconds;
                return TimeSpan.FromSeconds(seconds);
            };
    }
}
