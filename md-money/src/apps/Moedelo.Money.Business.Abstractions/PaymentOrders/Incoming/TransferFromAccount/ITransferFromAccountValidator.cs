using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount
{
    public interface ITransferFromAccountValidator
    {
        Task ValidateAsync(TransferFromAccountSaveRequest request);

        Task ValidateAsync(TransferFromAccountShortSaveRequest request);
    }
}