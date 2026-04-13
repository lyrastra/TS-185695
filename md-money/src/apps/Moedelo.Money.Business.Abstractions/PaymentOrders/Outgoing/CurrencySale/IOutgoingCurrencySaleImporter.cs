using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencySale;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale
{
    public interface IOutgoingCurrencySaleImporter
    {
        Task ImportAsync(OutgoingCurrencySaleImportRequest request);
    }
}