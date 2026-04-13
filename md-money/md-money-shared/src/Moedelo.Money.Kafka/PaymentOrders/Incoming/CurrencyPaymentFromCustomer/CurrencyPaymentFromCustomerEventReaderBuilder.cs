using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    [InjectAsSingleton(typeof(ICurrencyPaymentFromCustomerEventReaderBuilder))]
    public class CurrencyPaymentFromCustomerEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ICurrencyPaymentFromCustomerEventReaderBuilder
    {
        public CurrencyPaymentFromCustomerEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.CurrencyPaymentFromCustomer.Event.Topic,
                  MoneyTopics.PaymentOrders.CurrencyPaymentFromCustomer.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ICurrencyPaymentFromCustomerEventReaderBuilder OnCreated(Func<CurrencyPaymentFromCustomerCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ICurrencyPaymentFromCustomerEventReaderBuilder OnUpdated(Func<CurrencyPaymentFromCustomerUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ICurrencyPaymentFromCustomerEventReaderBuilder OnDeleted(Func<CurrencyPaymentFromCustomerDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ICurrencyPaymentFromCustomerEventReaderBuilder OnProvideRequired(Func<CurrencyPaymentFromCustomerProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}