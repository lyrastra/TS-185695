using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.CashCollection.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.CashCollection
{
    /// <summary>
    /// РКО - "Инкассация денег". Чтение событий
    /// </summary>
    public interface ICashCollectionEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ICashCollectionEventReaderBuilder OnCreated(Func<CashCollectionCreated, Task> onEvent);

        ICashCollectionEventReaderBuilder OnUpdated(Func<CashCollectionUpdated, Task> onEvent);

        ICashCollectionEventReaderBuilder OnDeleted(Func<CashCollectionDeleted, Task> onEvent);
    }
}
