using System;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces.ProducingRetry;
using Moedelo.Infrastructure.Kafka.Abstractions.Models.ProducingRetry;

namespace Moedelo.Common.Kafka.Retries;

internal sealed class EmptyKafkaProducingRetryPolicy : IKafkaProducingRetryPolicy
{
    internal static readonly IKafkaProducingRetryPolicy Instance = new EmptyKafkaProducingRetryPolicy();

    public KafkaProducingRetryState CreateProducingRetryState(string topicName)
    {
        return new KafkaProducingRetryState(topicName);
    }

    public KafkaProducingRetryState OnException(KafkaProducingRetryState retryState, Exception exception)
    {
        return new KafkaProducingRetryState(
            retryState.TopicName,
            retryState.ErrorsCount + 1,
            mustRetry: false,
            pauseBeforeRetry: null);
    }
}