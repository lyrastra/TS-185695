using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPurchase
{
    public interface IIncomingCurrencyPurchaseValidator
    {
        Task ValidateAsync(IncomingCurrencyPurchaseSaveRequest request);
    }
}