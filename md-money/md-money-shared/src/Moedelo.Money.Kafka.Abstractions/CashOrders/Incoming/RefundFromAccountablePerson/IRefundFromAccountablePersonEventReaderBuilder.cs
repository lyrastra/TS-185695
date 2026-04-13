using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RefundFromAccountablePerson.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RefundFromAccountablePerson
{
    /// <summary>
    /// ПКО - "Возврат от подотчетного лица". Чтение событий
    /// </summary>
    public interface IRefundFromAccountablePersonEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IRefundFromAccountablePersonEventReaderBuilder OnCreated(Func<RefundFromAccountablePersonCreated, Task> onEvent);

        IRefundFromAccountablePersonEventReaderBuilder OnUpdated(Func<RefundFromAccountablePersonUpdated, Task> onEvent);

        IRefundFromAccountablePersonEventReaderBuilder OnDeleted(Func<RefundFromAccountablePersonDeleted, Task> onEvent);

        IRefundFromAccountablePersonEventReaderBuilder OnProvided(Func<RefundFromAccountablePersonProvided, Task> onEvent);
    }
}
