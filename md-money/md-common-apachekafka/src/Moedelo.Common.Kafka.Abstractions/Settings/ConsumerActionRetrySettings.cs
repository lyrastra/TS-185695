using System;
using System.Collections.Generic;

namespace Moedelo.Common.Kafka.Abstractions.Settings
{
    public sealed class ConsumerActionRetrySettings
    {
        public ConsumerActionRetrySettings(
            int maxRetryCount,
            TimeSpan retryTimeout,
            IReadOnlyCollection<Type> exceptionsTypes = null,
            Func<TimeSpan, int, TimeSpan> retryTimeoutStrategy = null)
        {
            MaxRetryCount = maxRetryCount;
            RetryTimeout = retryTimeout;
            ExceptionsTypes = exceptionsTypes;
            RetryTimeoutStrategy = retryTimeoutStrategy;
        }

        public int MaxRetryCount { get; }

        public TimeSpan RetryTimeout { get; }

        /// <summary>
        /// Стратегия пользовательского изменения таймаута.
        /// Может быть полезна, если по какой-то причине нужно динамически менять время между попытками.
        /// На вход принимает таймаут, заданный в <see cref="RetryTimeout"/> и номер текущей попытки.
        /// Возвращает новый таймаут который будет использован вместо <see cref="RetryTimeout"/>.
        /// </summary>
        public Func<TimeSpan, int, TimeSpan> RetryTimeoutStrategy { get; set; }

        public IReadOnlyCollection<Type> ExceptionsTypes { get; }
    }
}