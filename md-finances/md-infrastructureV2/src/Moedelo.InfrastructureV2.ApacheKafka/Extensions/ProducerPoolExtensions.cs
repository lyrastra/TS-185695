using System;
using Moedelo.InfrastructureV2.ApacheKafka.Abstractions;

namespace Moedelo.InfrastructureV2.ApacheKafka.Extensions;

public static class ProducerPoolExtensions
{
    public static void EnsureProducerCanBeCreated(this IProducerPool producerPool, string brokerEndpoints)
    {
        if (producerPool.GetProducer(brokerEndpoints) == null)
            throw new Exception("Unable create producer");
    }
}