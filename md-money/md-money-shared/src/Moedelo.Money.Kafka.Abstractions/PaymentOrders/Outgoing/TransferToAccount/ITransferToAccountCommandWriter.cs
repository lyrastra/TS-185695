using System.Threading.Tasks;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount
{
    public interface ITransferToAccountCommandWriter
    {
        Task WriteImportAsync(ImportTransferToAccount commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateTransferToAccount commandData);

        Task WriteApplyIgnoreNumberAsync(ApplyIgnoreNumberTransferToAccount commandData);
    }
}
