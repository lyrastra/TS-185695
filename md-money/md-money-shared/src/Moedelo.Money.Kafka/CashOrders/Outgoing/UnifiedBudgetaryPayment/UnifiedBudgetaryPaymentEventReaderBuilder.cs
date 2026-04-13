using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentEventReaderBuilder))]
    class UnifiedBudgetaryPaymentEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IUnifiedBudgetaryPaymentEventReaderBuilder
    {
        public UnifiedBudgetaryPaymentEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.UnifiedBudgetaryPayment.Event.Topic,
                MoneyTopics.CashOrders.UnifiedBudgetaryPayment.EntityName,
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
    }
}
