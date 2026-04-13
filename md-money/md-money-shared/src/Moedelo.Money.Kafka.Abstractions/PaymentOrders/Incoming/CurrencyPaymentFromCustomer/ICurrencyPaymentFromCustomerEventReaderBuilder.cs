using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    public interface ICurrencyPaymentFromCustomerEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ICurrencyPaymentFromCustomerEventReaderBuilder OnCreated(Func<CurrencyPaymentFromCustomerCreated, Task> onEvent);
        ICurrencyPaymentFromCustomerEventReaderBuilder OnUpdated(Func<CurrencyPaymentFromCustomerUpdated, Task> onEvent);
        ICurrencyPaymentFromCustomerEventReaderBuilder OnDeleted(Func<CurrencyPaymentFromCustomerDeleted, Task> onEvent);

        ICurrencyPaymentFromCustomerEventReaderBuilder OnProvideRequired(Func<CurrencyPaymentFromCustomerProvideRequired, Task> onEvent);
    }
}