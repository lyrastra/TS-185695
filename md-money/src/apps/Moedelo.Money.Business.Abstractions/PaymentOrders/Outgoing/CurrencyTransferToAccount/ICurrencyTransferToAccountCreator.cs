using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    public interface ICurrencyTransferToAccountCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(CurrencyTransferToAccountSaveRequest saveRequest);
    }
}