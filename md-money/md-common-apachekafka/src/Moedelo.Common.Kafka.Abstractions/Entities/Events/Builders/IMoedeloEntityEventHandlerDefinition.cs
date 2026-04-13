using Moedelo.Common.Kafka.Abstractions.Entities.Messages;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders
{
    public interface IMoedeloEntityEventHandlerDefinition : IMoedeloEntityMessageHandlerDefinition<IMoedeloEntityEventHandlerDefinition, MoedeloEntityEventKafkaMessageValue>
    {
    }
}