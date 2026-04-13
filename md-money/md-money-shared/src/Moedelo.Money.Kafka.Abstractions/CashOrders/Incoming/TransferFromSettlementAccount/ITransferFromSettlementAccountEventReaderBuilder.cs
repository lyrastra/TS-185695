using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromSettlementAccount.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromSettlementAccount
{
    /// <summary>
    /// ПКО - "Поступление с расчётного счёта". Чтение событий
    /// </summary>
    public interface ITransferFromSettlementAccountEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ITransferFromSettlementAccountEventReaderBuilder OnCreated(Func<TransferFromSettlementAccountCreated, Task> onEvent);

        ITransferFromSettlementAccountEventReaderBuilder OnUpdated(Func<TransferFromSettlementAccountUpdated, Task> onEvent);

        ITransferFromSettlementAccountEventReaderBuilder OnDeleted(Func<TransferFromSettlementAccountDeleted, Task> onEvent);
    }
}
