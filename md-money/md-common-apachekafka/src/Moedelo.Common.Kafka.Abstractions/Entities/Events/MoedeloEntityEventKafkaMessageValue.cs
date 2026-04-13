using System;
using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Events
{
    public sealed class MoedeloEntityEventKafkaMessageValue : MoedeloKafkaMessageValueBase
    {
        public MoedeloEntityEventKafkaMessageValue(string entityType, string eventType, object eventData, string transmittedRef = null)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            if (string.IsNullOrWhiteSpace(eventType))
            {
                throw new ArgumentNullException(nameof(eventType));
            }

            if (transmittedRef == null && eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            EntityType = entityType;
            EventType = eventType;
            EventData = transmittedRef == null ? eventData : null;
            TransmittedRef = transmittedRef;
        }

        public string EntityType { get; }

        public string EventType { get; }

        public object EventData { get; set; }
    }
}