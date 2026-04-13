using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements;
using Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Events;
using Moedelo.Docs.Kafka.Abstractions.Topics;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;

namespace Moedelo.Docs.Kafka.Purchases.AdvanceStatements
{
    [InjectAsSingleton(typeof(IAdvanceStatementsEventReaderBuilder))]
    class AdvanceStatementsEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IAdvanceStatementsEventReaderBuilder
    {
        public AdvanceStatementsEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                AccountingPrimaryDocumentsTopics.Purchases.AdvanceStatements.CUD,
                AccountingPrimaryDocumentsTopics.Purchases.AdvanceStatements.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IAdvanceStatementsEventReaderBuilder OnPaymentToSupplierCreated(Func<PaymentToSupplierAdvanceStatementCreatedMessage, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IAdvanceStatementsEventReaderBuilder OnPaymentToSupplierUpdated(Func<PaymentToSupplierAdvanceStatementUpdatedMessage, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IAdvanceStatementsEventReaderBuilder OnDeleted(Func<AdvanceStatementDeletedMessage, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IAdvanceStatementsEventReaderBuilder OnBusinessTripCreated(Func<BusinessTripAdvanceStatementCreatedMessage, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IAdvanceStatementsEventReaderBuilder OnBusinessTripUpdated(Func<BusinessTripAdvanceStatementUpdatedMessage, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IAdvanceStatementsEventReaderBuilder OnGoodsAndServicesCreated(Func<GoodsAndServicesAdvanceStatementCreatedMessage, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IAdvanceStatementsEventReaderBuilder OnGoodsAndServicesUpdated(Func<GoodsAndServicesAdvanceStatementUpdatedMessage, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
