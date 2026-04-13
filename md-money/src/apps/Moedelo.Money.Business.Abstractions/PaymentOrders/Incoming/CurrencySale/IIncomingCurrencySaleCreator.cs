using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencySale;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencySale
{
    public interface IIncomingCurrencySaleCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(IncomingCurrencySaleSaveRequest saveRequest);
    }
}