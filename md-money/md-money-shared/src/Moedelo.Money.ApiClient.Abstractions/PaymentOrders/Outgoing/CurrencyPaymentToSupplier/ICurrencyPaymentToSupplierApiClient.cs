using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Models;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    public interface ICurrencyPaymentToSupplierApiClient
    {
        Task<CurrencyPaymentToSupplierDto> GetByIdAsync(long documentBaseId);
    }
}