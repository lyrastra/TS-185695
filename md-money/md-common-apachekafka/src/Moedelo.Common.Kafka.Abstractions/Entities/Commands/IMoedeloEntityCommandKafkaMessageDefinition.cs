using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands
{
    public interface IMoedeloEntityCommandKafkaMessageDefinition<out TCommandData>: IMoedeloKafkaMessageDefinition
    {
        string EntityType { get; }
        
        string CommandType { get; }
        
        TCommandData CommandData { get; }
    }
}