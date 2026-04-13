using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;

namespace Moedelo.InfrastructureV2.ApacheKafka.Abstractions
{
    public interface IKafkaProducer
    {
        Task<string> ProduceAsync<T>(string brokerEndpoints, KafkaMessage<T> message) where T : KafkaMessageValueBase;
        Task ProduceAsync<T>(
            string brokerEndpoints,
            IEnumerable<KafkaMessage<T>> messages,
            bool flushProducer = false) where T : KafkaMessageValueBase;

        /// <summary>
        /// Удостовериться, что внутренний пул продюсеров может функционировать должным образом
        /// </summary>
        void EnsureRawProducerPoolIsHealthy(string brokerEndpoints);
    }
}