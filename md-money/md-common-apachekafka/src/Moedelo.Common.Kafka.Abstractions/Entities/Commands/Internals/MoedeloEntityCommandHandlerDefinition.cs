using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Messages;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Internals
{
    internal sealed class MoedeloEntityCommandHandlerDefinition
        : MoedeloEntityMessageHandlerDefinition<IMoedeloEntityCommandHandlerDefinition, MoedeloEntityCommandKafkaMessageValue>,
        IMoedeloEntityCommandHandlerDefinition
    {
        public MoedeloEntityCommandHandlerDefinition(
            string topicName,
            IExecutionInfoContextInitializer contextInitializer, 
            IExecutionInfoContextAccessor contextAccessor, 
            IAuditTracer auditTracer, 
            ILogger logger, 
            Func<MoedeloEntityCommandKafkaMessageValue, Task> onCommand)
            : base(topicName, contextInitializer, contextAccessor, auditTracer, logger, onCommand)
        {
        }

        private protected override IMoedeloEntityCommandHandlerDefinition Self => this;
    }
}