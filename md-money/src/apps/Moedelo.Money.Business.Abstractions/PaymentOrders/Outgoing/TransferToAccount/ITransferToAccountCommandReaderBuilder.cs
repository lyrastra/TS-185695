using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount
{
    public interface ITransferToAccountCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        ITransferToAccountCommandReaderBuilder OnImport(Func<ImportTransferToAccount, Task> onCommand);
        ITransferToAccountCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateTransferToAccount, Task> onCommand);
        ITransferToAccountCommandReaderBuilder OnApplyIgnoreNumber(Func<ApplyIgnoreNumberTransferToAccount, Task> onCommand);
    }
}
