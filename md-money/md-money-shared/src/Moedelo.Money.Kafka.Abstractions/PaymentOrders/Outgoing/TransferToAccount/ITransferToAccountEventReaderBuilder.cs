using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount
{
    public interface ITransferToAccountEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ITransferToAccountEventReaderBuilder OnCreated(Func<TransferToAccountCreated, Task> onEvent);
        ITransferToAccountEventReaderBuilder OnUpdated(Func<TransferToAccountUpdated, Task> onEvent);
        ITransferToAccountEventReaderBuilder OnDeleted(Func<TransferToAccountDeleted, Task> onEvent);

        ITransferToAccountEventReaderBuilder OnProvideRequired(Func<TransferToAccountProvideRequired, Task> onEvent);
    }
}