using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentEventReaderBuilder))]
    public class UnifiedBudgetaryPaymentEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IUnifiedBudgetaryPaymentEventReaderBuilder
    {
        public UnifiedBudgetaryPaymentEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.UnifiedBudgetaryPayment.Event.Topic,
                  MoneyTopics.PaymentOrders.UnifiedBudgetaryPayment.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IUnifiedBudgetaryPaymentEventReaderBuilder OnCreated(Func<UnifiedBudgetaryPaymentCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IUnifiedBudgetaryPaymentEventReaderBuilder OnUpdated(Func<UnifiedBudgetaryPaymentUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IUnifiedBudgetaryPaymentEventReaderBuilder OnDeleted(Func<UnifiedBudgetaryPaymentDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IUnifiedBudgetaryPaymentEventReaderBuilder OnProvideRequired(Func<UnifiedBudgetaryPaymentProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IUnifiedBudgetaryPaymentEventReaderBuilder OnUpdateAfterAccountingStatementCreated(Func<UnifiedBudgetaryPaymentUpdateAfterAccountingStatementCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}