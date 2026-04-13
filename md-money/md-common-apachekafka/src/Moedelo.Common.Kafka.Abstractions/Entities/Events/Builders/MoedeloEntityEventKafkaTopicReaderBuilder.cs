using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Internals;
using Moedelo.Common.Kafka.Abstractions.Entities.Messages;
using Moedelo.Common.Kafka.Abstractions.Extensions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Newtonsoft.Json.Linq;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders
{
    public class MoedeloEntityEventKafkaTopicReaderBuilder :
        MoedeloEntityMessageKafkaTopicReaderBuilder<IMoedeloEntityEventHandlerDefinition, MoedeloEntityEventKafkaMessageValue, IEntityEventData, IMoedeloEntityEventKafkaTopicReaderBuilder>,
        IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        private readonly IExecutionInfoContextInitializer contextInitializer;
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IAuditTracer auditTracer;
        
        protected MoedeloEntityEventKafkaTopicReaderBuilder(
            string topicName, 
            string entityType,
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer, 
            IExecutionInfoContextAccessor contextAccessor, 
            IAuditTracer auditTracer, 
            ILogger logger = null)  : base(topicName, entityType, reader, logger)
        {
            this.contextInitializer = contextInitializer.EnsureIsNotNull(nameof(contextInitializer));
            this.contextAccessor = contextAccessor.EnsureIsNotNull(nameof(contextAccessor));
            this.auditTracer = auditTracer.EnsureIsNotNull(nameof(auditTracer));
        }

        public IMoedeloEntityEventKafkaTopicReaderBuilder OnEvent<T>(
            Func<T, Task> onEvent, 
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null) 
            where T : IEntityEventData
        {
            OnMessage(onEvent, eventDefinitionAction);

            return this;
        }

        public IMoedeloEntityEventKafkaTopicReaderBuilder OnEvent<T>(
            Func<T, KafkaMessageValueMetadata, Task> onEvent, 
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null) 
            where T : IEntityEventData
        {
            OnMessage(onEvent, eventDefinitionAction);

            return this;
        }

        public IMoedeloEntityEventKafkaTopicReaderBuilder OnEventException(Func<MoedeloEntityEventKafkaMessageValue, Exception, Task> onException)
        {
            return OnException(onException);
        }

        private protected sealed override TMessage ConvertTo<TMessage>(MoedeloEntityEventKafkaMessageValue message)
        {
            if (message.EventData is JObject jObject)
            {
                return jObject.ToObject<TMessage>();
            }

            if (message.EventData is string)
            {
                return (message.EventData as string).FromJsonString<TMessage>();
            }

            throw new Exception($"Ожидается JObject, но получен {message.EventData?.GetType()}");
        }

        private protected sealed override IMoedeloEntityEventKafkaTopicReaderBuilder Self => this;

        private protected sealed override string GetMessageEntityType(MoedeloEntityEventKafkaMessageValue messageValue)
        {
            return messageValue.EntityType;
        }

        
        private protected sealed override string GetMessageTypeName<TConcreteMessage>()
        {
            return EntityEventTypeMapper.GetEventType<TConcreteMessage>();
        }

        private protected sealed override string GetMessageType(MoedeloEntityEventKafkaMessageValue messageValue)
        {
            return messageValue.EventType;
        }

        private protected sealed override IMoedeloEntityEventHandlerDefinition CreateDefinition(DefinitionSetup definitionSetup)
        {
            return new MoedeloEntityEventHandlerDefinition(
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

        private protected sealed override Func<MoedeloEntityEventKafkaMessageValue, Task> BuildHandler(
            IMoedeloEntityEventHandlerDefinition messageDefinition,
            CancellationToken cancellationToken)
        {
            return (messageDefinition as MoedeloEntityEventHandlerDefinition)!.Build(cancellationToken);
        }
    }
}