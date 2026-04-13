using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCash.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCash
{
    public interface ITransferFromCashCommandWriter
    {
        Task WriteImportAsync(ImportTransferFromCash commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateTransferFromCash commandData);
    }
}