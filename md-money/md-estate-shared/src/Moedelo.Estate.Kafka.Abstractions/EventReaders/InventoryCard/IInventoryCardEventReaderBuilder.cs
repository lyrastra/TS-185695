using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Estate.Kafka.Abstractions.Events.EventData.InventoryCard;

namespace Moedelo.Estate.Kafka.Abstractions.EventReaders.InventoryCard
{
    public interface IInventoryCardEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IInventoryCardEventReaderBuilder OnCreated(Func<InventoryCardCreatedMessage, Task> onEvent);
        IInventoryCardEventReaderBuilder OnUpdated(Func<InventoryCardUpdatedMessage, Task> onEvent);
        IInventoryCardEventReaderBuilder OnDeleted(Func<InventoryCardDeletedMessage, Task> onEvent);
    }
}