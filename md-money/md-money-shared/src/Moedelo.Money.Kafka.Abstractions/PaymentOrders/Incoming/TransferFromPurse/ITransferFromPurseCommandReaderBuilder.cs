using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromPurse.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromPurse
{
    // note: Должен использоваться только в md-money!
    public interface ITransferFromPurseCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        ITransferFromPurseCommandReaderBuilder OnImport(Func<ImportTransferFromPurse, Task> onCommand);
        ITransferFromPurseCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateTransferFromPurse, Task> onCommand);
    }
}