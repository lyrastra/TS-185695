using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromPurse.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromPurse
{
    public interface ITransferFromPurseCommandWriter
    {
        Task WriteImportAsync(ImportTransferFromPurse commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateTransferFromPurse commandData);
    }
}