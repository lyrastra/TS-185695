using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Common.Kafka.Abstractions.Entities.Messages;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders
{
    public interface IMoedeloEntityCommandKafkaTopicReaderBuilder : IMoedeloEntityMessageKafkaTopicReaderBuilder<MoedeloEntityCommandKafkaMessageValue, IMoedeloEntityCommandKafkaTopicReaderBuilder>
    {
        IMoedeloEntityCommandKafkaTopicReaderBuilder OnCommand<T>(
            Func<T, Task> onCommand,
            Action<IMoedeloEntityCommandHandlerDefinition> commandDefinitionAction = null) 
            where T : IEntityCommandData;

        IMoedeloEntityCommandKafkaTopicReaderBuilder OnCommand<TMessage>(
            Func<TMessage, KafkaMessageValueMetadata, Task> onCommand,
            Action<IMoedeloEntityCommandHandlerDefinition> commandDefinitionAction = null)
            where TMessage : IEntityCommandData;
        
        IMoedeloEntityCommandKafkaTopicReaderBuilder OnCommandWithReply<T, TR>(
            Func<T, Task<TR>> onCommandWithReply,
            Action<IMoedeloEntityCommandHandlerDefinition> commandDefinitionAction = null)
            where T : IEntityCommandData
            where TR : IEntityCommandReplyData;
        
        IMoedeloEntityCommandKafkaTopicReaderBuilder OnCommandWithReply<T, TR>(
            Func<T, ReplyTo, Task<TR>> onCommandWithReply,
            Action<IMoedeloEntityCommandHandlerDefinition> commandDefinitionAction = null)
            where T : IEntityCommandData
            where TR : IEntityCommandReplyData;

        IMoedeloEntityCommandKafkaTopicReaderBuilder OnCommandException(
            Func<MoedeloEntityCommandKafkaMessageValue, Exception, Task> onException);
    }
}