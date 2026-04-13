using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.RefundToCustomer
{
    [InjectAsSingleton(typeof(IRefundToCustomerEventReaderBuilder))]
    public class RefundToCustomerEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IRefundToCustomerEventReaderBuilder
    {
        public RefundToCustomerEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.RefundToCustomer.Event.Topic,
                  MoneyTopics.PaymentOrders.RefundToCustomer.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IRefundToCustomerEventReaderBuilder OnCreated(Func<RefundToCustomerCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IRefundToCustomerEventReaderBuilder OnUpdated(Func<RefundToCustomerUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IRefundToCustomerEventReaderBuilder OnDeleted(Func<RefundToCustomerDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IRefundToCustomerEventReaderBuilder OnProvideRequired(Func<RefundToCustomerProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}