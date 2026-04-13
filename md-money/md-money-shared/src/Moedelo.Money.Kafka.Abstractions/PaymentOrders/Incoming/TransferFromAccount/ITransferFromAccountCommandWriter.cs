using System.Threading.Tasks;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromAccount.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromAccount
{
    public interface ITransferFromAccountCommandWriter
    {
        Task WriteImportAsync(ImportTransferFromAccount commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateTransferFromAccount commandData);

        Task WriteImportAmbiguousOperationTypeAsync(ImportAmbiguousOperationTypeTransferFromAccount commandData);
    }
}