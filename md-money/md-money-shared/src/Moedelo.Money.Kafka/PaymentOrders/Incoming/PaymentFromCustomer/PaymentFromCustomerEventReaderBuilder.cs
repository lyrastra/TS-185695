using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerEventReaderBuilder))]
    public class PaymentFromCustomerEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentFromCustomerEventReaderBuilder
    {
        public PaymentFromCustomerEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.PaymentFromCustomer.Event.Topic,
                  MoneyTopics.PaymentOrders.PaymentFromCustomer.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IPaymentFromCustomerEventReaderBuilder OnCreated(Func<PaymentFromCustomerCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentFromCustomerEventReaderBuilder OnUpdated(Func<PaymentFromCustomerUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentFromCustomerEventReaderBuilder OnDeleted(Func<PaymentFromCustomerDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentFromCustomerEventReaderBuilder OnProvideRequired(Func<PaymentFromCustomerProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentFromCustomerEventReaderBuilder OnSetReserve(Func<PaymentFromCustomerSetReserve, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}