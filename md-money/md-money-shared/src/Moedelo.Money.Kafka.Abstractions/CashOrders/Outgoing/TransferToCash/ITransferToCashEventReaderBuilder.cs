using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.TransferToCash.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.TransferToCash
{
    /// <summary>
    /// РКО - "Перевод в другую кассу". Чтение событий
    /// </summary>
    public interface ITransferToCashEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ITransferToCashEventReaderBuilder OnCreated(Func<TransferToCashCreated, Task> onEvent);

        ITransferToCashEventReaderBuilder OnUpdated(Func<TransferToCashUpdated, Task> onEvent);

        ITransferToCashEventReaderBuilder OnDeleted(Func<TransferToCashDeleted, Task> onEvent);
    }
}
