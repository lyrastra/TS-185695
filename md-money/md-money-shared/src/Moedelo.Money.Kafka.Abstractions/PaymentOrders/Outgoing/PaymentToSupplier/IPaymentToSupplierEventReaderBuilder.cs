using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IPaymentToSupplierEventReaderBuilder OnCreated(Func<PaymentToSupplierCreated, Task> onEvent);
        IPaymentToSupplierEventReaderBuilder OnUpdated(Func<PaymentToSupplierUpdated, Task> onEvent);
        IPaymentToSupplierEventReaderBuilder OnDeleted(Func<PaymentToSupplierDeleted, Task> onEvent);

        IPaymentToSupplierEventReaderBuilder OnProvideRequired(Func<PaymentToSupplierProvideRequired, Task> onEvent);
        IPaymentToSupplierEventReaderBuilder OnSetReserveAsync(Func<PaymentToSupplierSetReserve, Task> onEvent);
    }
}