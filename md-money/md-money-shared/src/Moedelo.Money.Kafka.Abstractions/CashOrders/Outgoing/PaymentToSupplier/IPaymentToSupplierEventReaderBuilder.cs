using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToSupplier.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IPaymentToSupplierEventReaderBuilder OnCreated(Func<PaymentToSupplierCreated, Task> onEvent);

        IPaymentToSupplierEventReaderBuilder OnUpdated(Func<PaymentToSupplierUpdated, Task> onEvent);

        IPaymentToSupplierEventReaderBuilder OnDeleted(Func<PaymentToSupplierDeleted, Task> onEvent);

        IPaymentToSupplierEventReaderBuilder OnProvided(Func<PaymentToSupplierProvided, Task> onEvent);
    }
}
