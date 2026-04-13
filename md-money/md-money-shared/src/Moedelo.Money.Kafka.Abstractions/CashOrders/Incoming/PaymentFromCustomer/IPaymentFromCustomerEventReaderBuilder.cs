using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.PaymentFromCustomer.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RetailRevenue.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IPaymentFromCustomerEventReaderBuilder OnCreated(Func<PaymentFromCustomerCreated, Task> onEvent);

        IPaymentFromCustomerEventReaderBuilder OnUpdated(Func<PaymentFromCustomerUpdated, Task> onEvent);

        IPaymentFromCustomerEventReaderBuilder OnDeleted(Func<PaymentFromCustomerDeleted, Task> onEvent);

        IPaymentFromCustomerEventReaderBuilder OnProvided(Func<PaymentFromCustomerProvided, Task> onEvent);
    }
}
