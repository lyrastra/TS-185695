using Confluent.Kafka;

namespace Moedelo.InfrastructureV2.ApacheKafka.Abstractions
{
    public interface IProducerPool
    {
        IProducer<string, string> GetProducer(string brokerEndpoints);
    }
}