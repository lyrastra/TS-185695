using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPurchase;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase
{
    public interface IOutgoingCurrencyPurchaseImporter
    {
        Task ImportAsync(OutgoingCurrencyPurchaseImportRequest request);
    }
}