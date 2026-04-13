using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromCash.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromCash
{
    /// <summary>
    /// ПКО - "Перемещение из другой кассы". Чтение событий
    /// </summary>
    public interface ITransferFromCashEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ITransferFromCashEventReaderBuilder OnCreated(Func<TransferFromCashCreated, Task> onEvent);

        ITransferFromCashEventReaderBuilder OnUpdated(Func<TransferFromCashUpdated, Task> onEvent);

        ITransferFromCashEventReaderBuilder OnDeleted(Func<TransferFromCashDeleted, Task> onEvent);
    }
}
