using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Models;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    public interface ICurrencyPaymentFromCustomerApiClient
    {
        Task<CurrencyPaymentFromCustomerDto> GetByIdAsync(long documentBaseId);
    }
}