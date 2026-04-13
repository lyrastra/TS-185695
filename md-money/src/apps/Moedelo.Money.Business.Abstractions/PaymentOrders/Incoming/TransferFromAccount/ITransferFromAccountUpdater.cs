using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount
{
    public interface ITransferFromAccountUpdater
    {
        Task<PaymentOrderSaveResponse> UpdateAsync(TransferFromAccountSaveRequest request);

        Task UpdateSumAsync(long documentBaseId, decimal sum);
    }
}