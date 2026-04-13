using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Incoming.PaymentFromCustomer.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PurseOperations.Incoming.PaymentFromCustomer
{
    /// <summary>
    /// Платежные системы - "Оплата поставщику". Чтение событий
    /// </summary>
    public interface IPaymentFromCustomerEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IPaymentFromCustomerEventReaderBuilder OnCreated(Func<PaymentFromCustomerCreated, Task> onEvent);

        IPaymentFromCustomerEventReaderBuilder OnUpdated(Func<PaymentFromCustomerUpdated, Task> onEvent);

        IPaymentFromCustomerEventReaderBuilder OnDeleted(Func<PaymentFromCustomerDeleted, Task> onEvent);

        IPaymentFromCustomerEventReaderBuilder OnProvided(Func<PaymentFromCustomerProvided, Task> onEvent);
    }
}