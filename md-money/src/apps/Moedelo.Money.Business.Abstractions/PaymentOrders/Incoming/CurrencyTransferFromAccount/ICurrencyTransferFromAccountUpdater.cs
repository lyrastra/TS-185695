using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    public interface ICurrencyTransferFromAccountUpdater
    {
        Task<PaymentOrderSaveResponse> UpdateAsync(CurrencyTransferFromAccountSaveRequest shortRequest);
    }
}