using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPurchase;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase
{
    public interface IOutgoingCurrencyPurchaseCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(OutgoingCurrencyPurchaseSaveRequest request);
    }
}