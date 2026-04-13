using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyPurchase
{
    [InjectAsSingleton(typeof(IOutgoingCurrencyPurchaseEventReaderBuilder))]
    public class OutgoingCurrencyPurchaseEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IOutgoingCurrencyPurchaseEventReaderBuilder
    {
        public OutgoingCurrencyPurchaseEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.OutgoingCurrencyPurchase.Event.Topic,
                  MoneyTopics.PaymentOrders.OutgoingCurrencyPurchase.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IOutgoingCurrencyPurchaseEventReaderBuilder OnCreated(Func<OutgoingCurrencyPurchaseCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IOutgoingCurrencyPurchaseEventReaderBuilder OnUpdated(Func<OutgoingCurrencyPurchaseUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IOutgoingCurrencyPurchaseEventReaderBuilder OnDeleted(Func<OutgoingCurrencyPurchaseDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IOutgoingCurrencyPurchaseEventReaderBuilder OnProvideRequired(Func<OutgoingCurrencyPurchaseProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}