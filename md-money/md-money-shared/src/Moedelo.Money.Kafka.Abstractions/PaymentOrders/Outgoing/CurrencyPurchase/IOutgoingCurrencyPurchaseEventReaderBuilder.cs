using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase
{
    public interface IOutgoingCurrencyPurchaseEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IOutgoingCurrencyPurchaseEventReaderBuilder OnCreated(Func<OutgoingCurrencyPurchaseCreated, Task> onEvent);
        IOutgoingCurrencyPurchaseEventReaderBuilder OnUpdated(Func<OutgoingCurrencyPurchaseUpdated, Task> onEvent);
        IOutgoingCurrencyPurchaseEventReaderBuilder OnDeleted(Func<OutgoingCurrencyPurchaseDeleted, Task> onEvent);

        IOutgoingCurrencyPurchaseEventReaderBuilder OnProvideRequired(Func<OutgoingCurrencyPurchaseProvideRequired, Task> onEvent);
    }
}