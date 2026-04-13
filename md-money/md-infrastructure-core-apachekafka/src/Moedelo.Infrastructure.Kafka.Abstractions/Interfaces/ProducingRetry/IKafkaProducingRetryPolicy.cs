using System;
using Moedelo.Infrastructure.Kafka.Abstractions.Models.ProducingRetry;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Interfaces.ProducingRetry
{
    public interface IKafkaProducingRetryPolicy
    {
        KafkaProducingRetryState CreateProducingRetryState(string topicName);
        KafkaProducingRetryState OnException(KafkaProducingRetryState retryState, Exception exception);
    }
}
