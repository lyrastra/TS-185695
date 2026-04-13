using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToNaturalPersons.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToNaturalPersons
{
    /// <summary>
    /// РКО - "Выплаты физ. лицам". Чтение событий
    /// </summary>
    public interface IPaymentToNaturalPersonsEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IPaymentToNaturalPersonsEventReaderBuilder OnCreated(Func<PaymentToNaturalPersonsCreated, Task> onEvent);

        IPaymentToNaturalPersonsEventReaderBuilder OnUpdated(Func<PaymentToNaturalPersonsUpdated, Task> onEvent);

        IPaymentToNaturalPersonsEventReaderBuilder OnDeleted(Func<PaymentToNaturalPersonsDeleted, Task> onEvent);
    }
}
