using System;

namespace Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;

public sealed class MoedeloEntityEventKafkaMessageValue<T> : MoedeloKafkaMessageValueBase
{
    public MoedeloEntityEventKafkaMessageValue(string entityType, string eventType, T eventData)
    {
        if (string.IsNullOrWhiteSpace(entityType))
        {
            throw new ArgumentNullException(nameof(entityType));
        }

        if (string.IsNullOrWhiteSpace(eventType))
        {
            throw new ArgumentNullException(nameof(eventType));
        }

        if (eventData == null)
        {
            throw new ArgumentNullException(nameof(eventData));
        }

        EntityType = entityType;
        EventType = eventType;
        EventData = eventData;
    }

    public string EntityType { get; }

    public string EventType { get; }

    public T EventData { get; }
}