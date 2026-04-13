using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.PaymentOrderImport.Kafka.Abstractions;
using Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements;
using Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements.Events;

namespace Moedelo.PaymentOrderImport.Kafka.Movements
{
    [InjectAsSingleton(typeof(IImportMovementStatusEventReaderBuilder))]
    internal sealed class ImportMovementStatusEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IImportMovementStatusEventReaderBuilder
    {
        public ImportMovementStatusEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                ImportTopics.Movement.Status.Event.Topic,
                ImportTopics.Movement.Status.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IImportMovementStatusEventReaderBuilder OnMovementParsingCompleted(Func<MovementParsingCompleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IImportMovementStatusEventReaderBuilder OnMovementParsingFailed(Func<MovementParsingFailed, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IImportMovementStatusEventReaderBuilder OnMovementImportCompleted(Func<MovementImportCompleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
