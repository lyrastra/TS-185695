using Confluent.Kafka;

namespace Moedelo.Infrastructure.Kafka.Abstractions
{
    internal interface IProducerPool
    {
        IProducer<string, string> GetProducer(string brokerEndpoints);
    }
}