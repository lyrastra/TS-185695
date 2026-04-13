using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Internals;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Common.Kafka.Abstractions.Entities.Messages;
using Moedelo.Common.Kafka.Abstractions.Extensions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Newtonsoft.Json.Linq;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders
{
    public abstract class MoedeloEntityCommandKafkaTopicReaderBuilder
        : MoedeloEntityMessageKafkaTopicReaderBuilder<IMoedeloEntityCommandHandlerDefinition,
                MoedeloEntityCommandKafkaMessageValue, IEntityCommandData,
                IMoedeloEntityCommandKafkaTopicReaderBuilder>,
            IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        private readonly IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter;
        private readonly IExecutionInfoContextInitializer contextInitializer;
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IAuditTracer auditTracer;

        protected MoedeloEntityCommandKafkaTopicReaderBuilder(
            string topicName,
            string entityType,
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer,
            ILogger logger = null) : base(topicName, entityType, reader, logger)
        {
            this.replyWriter = replyWriter.EnsureIsNotNull(nameof(replyWriter));
            this.contextInitializer = contextInitializer.EnsureIsNotNull(nameof(contextInitializer));
            this.contextAccessor = contextAccessor.EnsureIsNotNull(nameof(contextAccessor));
            this.auditTracer = auditTracer.EnsureIsNotNull(nameof(auditTracer));
        }

        public IMoedeloEntityCommandKafkaTopicReaderBuilder OnCommand<TMessage>(
            Func<TMessage, Task> onCommand,
            Action<IMoedeloEntityCommandHandlerDefinition> commandDefinitionAction = null)
            where TMessage : IEntityCommandData
        {
            OnMessage(onCommand, commandDefinitionAction);

            return this;
        }

        public IMoedeloEntityCommandKafkaTopicReaderBuilder OnCommand<TMessage>(
            Func<TMessage, KafkaMessageValueMetadata, Task> onCommand,
            Action<IMoedeloEntityCommandHandlerDefinition> commandDefinitionAction = null)
            where TMessage : IEntityCommandData
        {
            OnMessage(onCommand, commandDefinitionAction);

            return this;
        }

        public IMoedeloEntityCommandKafkaTopicReaderBuilder OnCommandWithReply<TMessage, TReply>(
            Func<TMessage, Task<TReply>> onCommandWithReply,
            Action<IMoedeloEntityCommandHandlerDefinition> commandDefinitionAction = null)
            where TMessage : IEntityCommandData
            where TReply : IEntityCommandReplyData
        {
            var func = replyWriter.WrapCommandWithReply(onCommandWithReply.EnsureIsNotNull(nameof(onCommandWithReply)));

            OnMessage(func, commandDefinitionAction);

            return this;
        }

        public IMoedeloEntityCommandKafkaTopicReaderBuilder OnCommandWithReply<TMessage, TReply>(
            Func<TMessage, ReplyTo, Task<TReply>> onCommandWithReply,
            Action<IMoedeloEntityCommandHandlerDefinition> commandDefinitionAction = null)
            where TMessage : IEntityCommandData
            where TReply : IEntityCommandReplyData
        {
            var func = replyWriter.WrapCommandWithReply(onCommandWithReply.EnsureIsNotNull(nameof(onCommandWithReply)));

            OnMessage(func, commandDefinitionAction);

            return this;
        }

        public IMoedeloEntityCommandKafkaTopicReaderBuilder OnCommandException(
            Func<MoedeloEntityCommandKafkaMessageValue, Exception, Task> onException)
        {
            return OnException(onException);
        }

        private protected sealed override TMessage ConvertTo<TMessage>(MoedeloEntityCommandKafkaMessageValue message)
        {
            if (message.CommandData is JObject jObject)
            {
                return jObject.ToObject<TMessage>();
            }

            if (message.CommandData is string)
            {
                return (message.CommandData as string).FromJsonString<TMessage>();
            }

            throw new Exception($"Ожидается JObject, но получен {message.CommandData?.GetType()}");
        }

        private protected sealed override IMoedeloEntityCommandKafkaTopicReaderBuilder Self => this;

        private protected sealed override string GetMessageEntityType(
            MoedeloEntityCommandKafkaMessageValue messageValue)
        {
            return messageValue.EntityType;
        }

        private protected sealed override string GetMessageTypeName<TConcreteMessage>()
        {
            return EntityCommandTypeMapper.GetCommandType<TConcreteMessage>();
        }

        private protected sealed override string GetMessageType(MoedeloEntityCommandKafkaMessageValue messageValue)
        {
            return messageValue.CommandType;
        }

        private protected sealed override IMoedeloEntityCommandHandlerDefinition CreateDefinition(
            DefinitionSetup definitionSetup)
        {
            return new MoedeloEntityCommandHandlerDefinition(
                    definitionSetup.TopicName,
                    contextInitializer,
                    contextAccessor,
                    auditTracer,
                    logger,
                    definitionSetup.MessageHandler)
                .SetWithRetry(definitionSetup.WithRetry, definitionSetup.RetrySettings)
                .SetDebugLogging(definitionSetup.WithDebugLogging)
                .SetOnMessageExceptionHandler(definitionSetup.OnException)
                .SetExecutionContextTokenFallback(definitionSetup.OnInvalidExecutionContextToken);
        }

        private protected sealed override Func<MoedeloEntityCommandKafkaMessageValue, Task> BuildHandler(
            IMoedeloEntityCommandHandlerDefinition messageDefinition,
            CancellationToken cancellationToken)
        {
            return (messageDefinition as MoedeloEntityCommandHandlerDefinition)!.Build(cancellationToken);
        }
    }
}
