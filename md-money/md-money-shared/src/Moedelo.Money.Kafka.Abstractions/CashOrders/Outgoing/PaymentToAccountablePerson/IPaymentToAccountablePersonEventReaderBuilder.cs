using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToAccountablePerson.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToAccountablePerson
{
    /// <summary>
    /// РКО - "Выдача подотчетному лицу". Чтение событий
    /// </summary>
    public interface IPaymentToAccountablePersonEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IPaymentToAccountablePersonEventReaderBuilder OnCreated(Func<PaymentToAccountablePersonCreated, Task> onEvent);

        IPaymentToAccountablePersonEventReaderBuilder OnUpdated(Func<PaymentToAccountablePersonUpdated, Task> onEvent);

        IPaymentToAccountablePersonEventReaderBuilder OnDeleted(Func<PaymentToAccountablePersonDeleted, Task> onEvent);

        IPaymentToAccountablePersonEventReaderBuilder OnProvided(Func<PaymentToAccountablePersonProvided, Task> onEvent);
    }
}