using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;

namespace Moedelo.InfrastructureV2.ApacheKafka.Implementations
{
    [InjectAsSingleton]
    public sealed class MoedeloEntityEventKafkaTopicWriter : IMoedeloEntityEventKafkaTopicWriter
    {
        private readonly IMoedeloKafkaTopicWriter kafkaTopicWriter;

        public MoedeloEntityEventKafkaTopicWriter(IMoedeloKafkaTopicWriter kafkaTopicWriter)
        {
            this.kafkaTopicWriter = kafkaTopicWriter;
        }

        public Task<string> WriteEventDataAsync<T>(string topicName, string eventKey, string entityType, T eventData, string contextToken) 
            where T : IEntityEventData
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            var messageValue = new MoedeloEntityEventKafkaMessageValue<T>(
                entityType, typeof(T).Name, eventData)
            {
                Token = contextToken
            };

            return kafkaTopicWriter.WriteAsync(topicName, eventKey, messageValue);
        }
    }
}