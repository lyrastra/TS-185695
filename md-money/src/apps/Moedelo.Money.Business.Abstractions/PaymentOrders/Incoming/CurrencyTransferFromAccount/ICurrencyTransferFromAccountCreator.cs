using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    public interface ICurrencyTransferFromAccountCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(CurrencyTransferFromAccountSaveRequest saveRequest);
    }
}