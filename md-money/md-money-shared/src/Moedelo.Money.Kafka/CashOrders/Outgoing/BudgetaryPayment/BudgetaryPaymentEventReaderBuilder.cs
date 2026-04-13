using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.BudgetaryPayment.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;

namespace Moedelo.Money.Kafka.CashOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentEventReaderBuilder))]
    class BudgetaryPaymentEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IBudgetaryPaymentEventReaderBuilder
    {
        public BudgetaryPaymentEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.BudgetaryPayment.Event.Topic,
                MoneyTopics.CashOrders.BudgetaryPayment.EntityName,
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
    }
}
