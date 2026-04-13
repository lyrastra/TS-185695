using System;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces.ProducingRetry;
using Moedelo.Infrastructure.Kafka.Abstractions.Models.ProducingRetry;

namespace Moedelo.Infrastructure.Kafka.ProducingRetry;

[InjectAsSingleton(typeof(IKafkaProducingRetryPolicy))]
internal sealed class DefaultKafkaProducingRetryPolicy : IKafkaProducingRetryPolicy
{
    private static readonly TimeSpan[] Pauses =
    {
        TimeSpan.FromSeconds(1),
        TimeSpan.FromSeconds(5),
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(10)
    };

    public KafkaProducingRetryState CreateProducingRetryState(string topicName)
    {
        return new KafkaProducingRetryState(topicName);
    }

    public KafkaProducingRetryState OnException(KafkaProducingRetryState retryState, Exception exception)
    {
        var errorsCount = retryState.ErrorsCount;
        if (errorsCount >= Pauses.Length)
        {
            return new KafkaProducingRetryState(
                retryState.TopicName,
                errorsCount + 1,
                mustRetry: false,
                pauseBeforeRetry: null);
        }

        return new KafkaProducingRetryState(
            retryState.TopicName,
            errorsCount + 1,
            mustRetry: true,
            pauseBeforeRetry: Pauses[errorsCount]);
    }
}
