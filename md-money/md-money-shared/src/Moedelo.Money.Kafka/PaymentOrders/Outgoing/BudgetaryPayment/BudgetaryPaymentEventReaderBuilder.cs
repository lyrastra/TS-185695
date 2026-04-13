using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentEventReaderBuilder))]
    public class BudgetaryPaymentEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IBudgetaryPaymentEventReaderBuilder
    {
        public BudgetaryPaymentEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.BudgetaryPayment.Event.Topic,
                  MoneyTopics.PaymentOrders.BudgetaryPayment.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IBudgetaryPaymentEventReaderBuilder OnCreated(Func<BudgetaryPaymentCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IBudgetaryPaymentEventReaderBuilder OnUpdated(Func<BudgetaryPaymentUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IBudgetaryPaymentEventReaderBuilder OnDeleted(Func<BudgetaryPaymentDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IBudgetaryPaymentEventReaderBuilder OnProvideRequired(Func<BudgetaryPaymentProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IBudgetaryPaymentEventReaderBuilder OnUpdateAfterAccountingStatementCreated(Func<BudgetaryPaymentUpdateAfterAccountingStatementCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}