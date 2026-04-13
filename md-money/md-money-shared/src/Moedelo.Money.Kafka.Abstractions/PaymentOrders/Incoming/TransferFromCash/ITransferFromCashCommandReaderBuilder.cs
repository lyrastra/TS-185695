using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCash.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCash
{
    // note: Должен использоваться только в md-money!
    public interface ITransferFromCashCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        ITransferFromCashCommandReaderBuilder OnImport(Func<ImportTransferFromCash, Task> onCommand);
        ITransferFromCashCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateTransferFromCash, Task> onCommand);
    }
}