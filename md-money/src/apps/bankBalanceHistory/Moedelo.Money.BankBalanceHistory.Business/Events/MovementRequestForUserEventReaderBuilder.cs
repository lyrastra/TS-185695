using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Events;
using Moedelo.PaymentOrderImport.Kafka.Abstractions;
using Moedelo.PaymentOrderImport.Kafka.Abstractions.ImportForUser.Events;

namespace Moedelo.Money.BankBalanceHistory.Business.Events
{
    [InjectAsSingleton]
    internal sealed class MovementRequestForUserEventReaderBuilder: MoedeloEntityEventKafkaTopicReaderBuilder, IMovementRequestForUserEventReaderBuilder
    {
        public MovementRequestForUserEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                ImportTopics.ImportForUser.Document.Event.Topic,
                ImportTopics.ImportForUser.Document.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }
        
        public IMovementRequestForUserEventReaderBuilder OnImportForUserEvent(Func<ImportForUserKafkaEvent, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}