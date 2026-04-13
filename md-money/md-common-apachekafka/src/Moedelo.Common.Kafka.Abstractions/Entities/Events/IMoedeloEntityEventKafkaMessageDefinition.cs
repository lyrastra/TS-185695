using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Events
{
    public interface IMoedeloEntityEventKafkaMessageDefinition<T> : IMoedeloKafkaMessageDefinition
    {
        string EntityType { get; }
        
        string EventType { get; }
        
        T EventData { get; }
    }
}