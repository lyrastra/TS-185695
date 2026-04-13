using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;

namespace Moedelo.InfrastructureV2.ApacheKafka.Implementations
{
    [InjectAsSingleton]
    public sealed class MoedeloEntityCommandKafkaTopicWriter : IMoedeloEntityCommandKafkaTopicWriter
    {
        private readonly IMoedeloKafkaTopicWriter kafkaTopicWriter;

        public MoedeloEntityCommandKafkaTopicWriter(IMoedeloKafkaTopicWriter kafkaTopicWriter)
        {
            this.kafkaTopicWriter = kafkaTopicWriter;
        }

        public Task<string> WriteCommandDataAsync<T>(string topicName, string commandKey, string entityType, T commandData, string contextToken) 
            where T : IEntityCommandData
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            if (commandData == null)
            {
                throw new ArgumentNullException(nameof(commandData));
            }

            var messageValue = new MoedeloEntityCommandKafkaMessageValue<T>(
                entityType, typeof(T).Name, commandData)
            {
                Token = contextToken,
            };
            
            return kafkaTopicWriter.WriteAsync(topicName, commandKey, messageValue);
        }
    }
}