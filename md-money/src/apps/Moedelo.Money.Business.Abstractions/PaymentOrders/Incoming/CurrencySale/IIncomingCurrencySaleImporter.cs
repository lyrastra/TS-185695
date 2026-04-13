using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencySale;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencySale
{
    public interface IIncomingCurrencySaleImporter
    {
        Task ImportAsync(IncomingCurrencySaleImportRequest request);
    }
}