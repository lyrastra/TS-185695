using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencySale
{
    [InjectAsSingleton(typeof(IOutgoingCurrencySaleEventReaderBuilder))]
    public class OutgoingCurrencySaleEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IOutgoingCurrencySaleEventReaderBuilder
    {
        public OutgoingCurrencySaleEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.OutgoingCurrencySale.Event.Topic,
                  MoneyTopics.PaymentOrders.OutgoingCurrencySale.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IOutgoingCurrencySaleEventReaderBuilder OnCreated(Func<OutgoingCurrencySaleCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IOutgoingCurrencySaleEventReaderBuilder OnUpdated(Func<OutgoingCurrencySaleUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IOutgoingCurrencySaleEventReaderBuilder OnDeleted(Func<OutgoingCurrencySaleDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IOutgoingCurrencySaleEventReaderBuilder OnProvideRequired(Func<OutgoingCurrencySaleProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}