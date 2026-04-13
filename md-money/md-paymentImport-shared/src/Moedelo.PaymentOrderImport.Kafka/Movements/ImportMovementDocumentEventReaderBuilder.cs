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
    [InjectAsSingleton(typeof(IImportMovementDocumentEventReaderBuilder))]
    class ImportMovementDocumentEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IImportMovementDocumentEventReaderBuilder
    {
        public ImportMovementDocumentEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                ImportTopics.Movement.Document.Event.Topic,
                ImportTopics.Movement.Document.EntityName,
                reader,
                contextInitializer, 
                contextAccessor,
                auditTracer)
        {
        }

        public IImportMovementDocumentEventReaderBuilder OnDocumentImportCompleted(Func<DocumentImportCompleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IImportMovementDocumentEventReaderBuilder OnDocumentImportFailed(Func<DocumentImportFailed, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IImportMovementDocumentEventReaderBuilder OnDocumentImportSkipped(Func<DocumentImportSkipped, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
