using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromAccount.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromAccount
{
    // note: Должен использоваться только в md-money!
    public interface ITransferFromAccountCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        ITransferFromAccountCommandReaderBuilder OnImport(Func<ImportTransferFromAccount, Task> onCommand);
        ITransferFromAccountCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateTransferFromAccount, Task> onCommand);
        ITransferFromAccountCommandReaderBuilder OnImportAmbiguousOperationType(Func<ImportAmbiguousOperationTypeTransferFromAccount, Task> onCommand);
    }
}