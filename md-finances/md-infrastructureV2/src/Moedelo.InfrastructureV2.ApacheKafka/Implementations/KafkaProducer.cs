using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using Moedelo.InfrastructureV2.ApacheKafka.Abstractions;
using Moedelo.InfrastructureV2.ApacheKafka.Extensions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.ApacheKafka.Implementations
{
    [InjectAsSingleton(typeof(IKafkaProducer))]
    public sealed class KafkaProducer : IKafkaProducer
    {
        private readonly IProducerPool producerPool;

        public KafkaProducer(IProducerPool producerPool)
        {
            this.producerPool = producerPool;
        }

        public async Task<string> ProduceAsync<T>(string brokerEndpoints, KafkaMessage<T> message)
            where T : KafkaMessageValueBase
        {
            var producer = producerPool.GetProducer(brokerEndpoints);
            var topic = message.TopicName;
            var messageProduce = new Message<string, string>()
            {
                Key = message.Key,
                Value = message.Value.ToJsonString()
            };

            var result = await producer.ProduceAsync(topic, messageProduce).ConfigureAwait(false);
            var tpo = result.TopicPartitionOffset;
            
            return tpo.ToString();
        }

        public Task ProduceAsync<T>(
            string brokerEndpoints,
            IEnumerable<KafkaMessage<T>> messages,
            bool flushProducer = false) where T : KafkaMessageValueBase
        {
            var producer = producerPool.GetProducer(brokerEndpoints);

            foreach (var message in messages)
            {
                var topic = message.TopicName;
                var key = message.Key;
                var value = message.Value.ToJsonString();

                producer.Produce(topic, new Message<string, string>
                {
                    Key = key,
                    Value = value,
                });
            }

            if (flushProducer)
            {
                var flashTimeout = TimeSpan.FromSeconds(2);
                producer.Flush(flashTimeout);
            }

            // пока оставил метод псевдо-асинхронным
            return Task.CompletedTask;
        }

        private readonly ConcurrentDictionary<string, bool> checkedEndpoints =
            new ConcurrentDictionary<string, bool>();

        private bool EnsureProducerCanBeCreated(string brokerEndpoints)
        {
            producerPool.EnsureProducerCanBeCreated(brokerEndpoints);

            return true;
        }

        public void EnsureRawProducerPoolIsHealthy(string brokerEndpoints)
        {
            checkedEndpoints.GetOrAdd(brokerEndpoints, EnsureProducerCanBeCreated);
        }
    }
}