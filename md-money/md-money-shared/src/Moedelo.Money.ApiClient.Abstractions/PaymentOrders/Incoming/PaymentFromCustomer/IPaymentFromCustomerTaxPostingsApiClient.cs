using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Dto;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerTaxPostingsApiClient
    {
        Task<string> GenerateAsync(PaymentFromCustomerTaxPostingsGenerateRequestDto generateRequest);
    }
}
