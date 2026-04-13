using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount
{
    public interface ITransferToAccountValidator
    {
        Task ValidateAsync(TransferToAccountSaveRequest request);
    }
}