using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencySale;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencySale
{
    public interface IIncomingCurrencySaleUpdater
    {
        Task<PaymentOrderSaveResponse>  UpdateAsync(IncomingCurrencySaleSaveRequest saveRequest);
    }
}
