using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale
{
    public interface IOutgoingCurrencySaleEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IOutgoingCurrencySaleEventReaderBuilder OnCreated(Func<OutgoingCurrencySaleCreated, Task> onEvent);
        IOutgoingCurrencySaleEventReaderBuilder OnUpdated(Func<OutgoingCurrencySaleUpdated, Task> onEvent);
        IOutgoingCurrencySaleEventReaderBuilder OnDeleted(Func<OutgoingCurrencySaleDeleted, Task> onEvent);

        IOutgoingCurrencySaleEventReaderBuilder OnProvideRequired(Func<OutgoingCurrencySaleProvideRequired, Task> onEvent);
    }
}