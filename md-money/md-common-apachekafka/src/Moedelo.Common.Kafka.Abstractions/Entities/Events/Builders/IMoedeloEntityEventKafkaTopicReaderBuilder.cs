using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Messages;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders
{
    public interface IMoedeloEntityEventKafkaTopicReaderBuilder  : IMoedeloEntityMessageKafkaTopicReaderBuilder<MoedeloEntityEventKafkaMessageValue, IMoedeloEntityEventKafkaTopicReaderBuilder>
    {
        IMoedeloEntityEventKafkaTopicReaderBuilder OnEvent<T>(
            Func<T, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null) 
            where T : IEntityEventData;

        public IMoedeloEntityEventKafkaTopicReaderBuilder OnEvent<T>(
            Func<T, KafkaMessageValueMetadata, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null)
            where T : IEntityEventData;

        IMoedeloEntityEventKafkaTopicReaderBuilder OnEventException(
            Func<MoedeloEntityEventKafkaMessageValue, Exception, Task> onException);
    }
}
