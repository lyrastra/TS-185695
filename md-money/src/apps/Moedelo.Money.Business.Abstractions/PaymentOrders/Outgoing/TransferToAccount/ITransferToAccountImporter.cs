using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount
{
    public interface ITransferToAccountImporter
    {
        Task ImportAsync(TransferToAccountImportRequest request);
    }
}