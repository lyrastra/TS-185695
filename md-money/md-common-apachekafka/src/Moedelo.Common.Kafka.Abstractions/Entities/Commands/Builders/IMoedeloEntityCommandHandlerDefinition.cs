using Moedelo.Common.Kafka.Abstractions.Entities.Messages;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders
{
    public interface IMoedeloEntityCommandHandlerDefinition : IMoedeloEntityMessageHandlerDefinition<IMoedeloEntityCommandHandlerDefinition, MoedeloEntityCommandKafkaMessageValue>
    {
    }
}