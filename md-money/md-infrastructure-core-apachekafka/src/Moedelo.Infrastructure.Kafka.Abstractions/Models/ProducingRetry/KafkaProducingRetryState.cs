using System;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Models.ProducingRetry
{
    public readonly struct KafkaProducingRetryState
    {
        public KafkaProducingRetryState(string topicName)
        {
            TopicName = topicName;
            ErrorsCount = 0;
            MustRetry = false;
            PauseBeforeRetry = null;
        }

        public KafkaProducingRetryState(string topicName, int errorsCount, bool mustRetry, TimeSpan? pauseBeforeRetry)
        {
            TopicName = topicName;
            ErrorsCount = errorsCount;
            MustRetry = mustRetry;
            PauseBeforeRetry = pauseBeforeRetry;
        }

        public string TopicName { get; }
        public int ErrorsCount { get; }
        public bool MustRetry { get; }
        public TimeSpan? PauseBeforeRetry { get; }
    }
}
