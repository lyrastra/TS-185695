using System;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Events
{
    internal sealed class MoedeloEntityEventKafkaMessageDefinition<T> : MoedeloKafkaMessageDefinitionBase, IMoedeloEntityEventKafkaMessageDefinition<T>
        where T : IEntityEventData
    {
        public MoedeloEntityEventKafkaMessageDefinition(
            string topicName,
            string entityType,
            string keyMessage,
            T eventData)
        {
            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new ArgumentNullException(nameof(topicName));
            }

            if (string.IsNullOrWhiteSpace(keyMessage))
            {
                throw new ArgumentNullException(nameof(keyMessage));
            }

            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            TopicName = topicName;
            KeyMessage = keyMessage;
            EntityType = entityType;
            EventType = EntityEventTypeMapper.GetEventType(eventData);
            EventData = eventData;
        }
        
        public string EntityType { get; }
        public string EventType { get; }
        public T EventData { get; }
    }
}