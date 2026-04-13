#nullable enable
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Messages;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Events.Internals
{
    internal sealed class MoedeloEntityEventHandlerDefinition
        : MoedeloEntityMessageHandlerDefinition<IMoedeloEntityEventHandlerDefinition, MoedeloEntityEventKafkaMessageValue>, 
            IMoedeloEntityEventHandlerDefinition
    {
        internal MoedeloEntityEventHandlerDefinition(
            string topicName,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer,
            ILogger? logger,
            Func<MoedeloEntityEventKafkaMessageValue, Task> onEvent) : base(topicName, contextInitializer, contextAccessor, auditTracer, logger, onEvent)
        {
        }

        private protected override IMoedeloEntityEventHandlerDefinition Self => this;
    }
}