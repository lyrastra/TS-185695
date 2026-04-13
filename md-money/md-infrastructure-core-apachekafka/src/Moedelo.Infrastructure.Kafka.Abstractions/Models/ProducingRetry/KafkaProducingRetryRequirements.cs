using System;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Models.ProducingRetry
{
    public readonly struct KafkaProducingRetryRequirements
    {
        public KafkaProducingRetryRequirements(
            bool mustRetry,
            TimeSpan? pauseBeforeRetry)
        {
            MustRetry = mustRetry;
            PauseBeforeRetry = pauseBeforeRetry;
        }

        public bool MustRetry { get; }
        public TimeSpan? PauseBeforeRetry { get; }
    }
}