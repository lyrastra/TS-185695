using System;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands
{
    internal sealed class MoedeloEntityCommandKafkaMessageDefinition<T> : MoedeloKafkaMessageDefinitionBase, IMoedeloEntityCommandKafkaMessageDefinition<T>
        where T : IEntityCommandData
    {
        public MoedeloEntityCommandKafkaMessageDefinition(
            string topicName,
            string entityType,
            string keyMessage,
            T commandData)
        {
            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new ArgumentNullException(nameof(topicName));
            }
            
            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }
            
            if (string.IsNullOrWhiteSpace(keyMessage))
            {
                throw new ArgumentNullException(nameof(keyMessage));
            }

            TopicName = topicName;
            EntityType = entityType;
            KeyMessage = keyMessage;
            CommandType = EntityCommandTypeMapper.GetCommandType(commandData);
            CommandData = commandData ?? throw new ArgumentNullException(nameof(commandData));
        }
        
        public string EntityType { get; }
        public string CommandType { get; }
        public T CommandData { get; }
    }
}