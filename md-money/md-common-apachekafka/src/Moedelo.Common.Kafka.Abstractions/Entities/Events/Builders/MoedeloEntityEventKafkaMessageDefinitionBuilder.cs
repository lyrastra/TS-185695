using System;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders
{
    public class MoedeloEntityEventKafkaMessageDefinitionBuilder
    {
        private readonly string topicName;
        private readonly string entityType;

        public static MoedeloEntityEventKafkaMessageDefinitionBuilder For(string topicName, string entityType)
        {
            return new MoedeloEntityEventKafkaMessageDefinitionBuilder(topicName, entityType);
        }

        private MoedeloEntityEventKafkaMessageDefinitionBuilder(string topicName, string entityType)
        {
            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new ArgumentException(nameof(topicName));
            }

            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentException(nameof(entityType));
            }

            this.topicName = topicName;
            this.entityType = entityType;
        }

        public IMoedeloEntityEventKafkaMessageDefinition<T> CreateEventDefinition<T>(string eventKey, T eventData)
            where T : IEntityEventData
        {
            return new MoedeloEntityEventKafkaMessageDefinition<T>(topicName, entityType, eventKey, eventData);
        }
    }
}