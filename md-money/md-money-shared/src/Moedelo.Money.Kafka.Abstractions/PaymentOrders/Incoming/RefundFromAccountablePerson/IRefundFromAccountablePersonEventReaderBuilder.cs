using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    public interface IRefundFromAccountablePersonEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IRefundFromAccountablePersonEventReaderBuilder OnCreated(Func<RefundFromAccountablePersonCreated, Task> onEvent);
        IRefundFromAccountablePersonEventReaderBuilder OnUpdated(Func<RefundFromAccountablePersonUpdated, Task> onEvent);
        IRefundFromAccountablePersonEventReaderBuilder OnDeleted(Func<RefundFromAccountablePersonDeleted, Task> onEvent);

        IRefundFromAccountablePersonEventReaderBuilder OnProvideRequired(Func<RefundFromAccountablePersonProvideRequired, Task> onEvent);
    }
}