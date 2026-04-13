using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.TransferToSettlementAccount.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.TransferToSettlementAccount
{
    /// <summary>
    /// Платежные системы - "Перевод на р/с". Чтение событий
    /// </summary>
    public interface ITransferToSettlementAccountEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ITransferToSettlementAccountEventReaderBuilder OnCreated(Func<TransferToSettlementAccountCreated, Task> onEvent);

        ITransferToSettlementAccountEventReaderBuilder OnUpdated(Func<TransferToSettlementAccountUpdated, Task> onEvent);

        ITransferToSettlementAccountEventReaderBuilder OnDeleted(Func<TransferToSettlementAccountDeleted, Task> onEvent);
    }
}
