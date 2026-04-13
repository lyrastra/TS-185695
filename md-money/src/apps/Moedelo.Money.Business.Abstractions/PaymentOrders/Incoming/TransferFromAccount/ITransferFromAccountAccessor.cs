using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount
{
    public interface ITransferFromAccountAccessor
    {
        Task<bool> IsReadOnlyAsync(TransferFromAccountResponse payment);
    }
}
