using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer
{
    public interface IRefundToCustomerEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IRefundToCustomerEventReaderBuilder OnCreated(Func<RefundToCustomerCreated, Task> onEvent);
        IRefundToCustomerEventReaderBuilder OnUpdated(Func<RefundToCustomerUpdated, Task> onEvent);
        IRefundToCustomerEventReaderBuilder OnDeleted(Func<RefundToCustomerDeleted, Task> onEvent);

        IRefundToCustomerEventReaderBuilder OnProvideRequired(Func<RefundToCustomerProvideRequired, Task> onEvent);
    }
}