using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPurchase
{
    public interface IIncomingCurrencyPurchaseImporter
    {
        Task ImportAsync(IncomingCurrencyPurchaseImportRequest request);
    }
}