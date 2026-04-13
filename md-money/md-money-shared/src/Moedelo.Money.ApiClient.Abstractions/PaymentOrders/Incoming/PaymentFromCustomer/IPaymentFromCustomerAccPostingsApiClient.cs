using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto.Providing.AccPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Dto;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerAccPostingsApiClient
    {
        Task<AccPostingsResponseDto> GenerateAsync(PaymentFromCustomerAccPostingsGenerateRequestDto generateRequest);
    }
}
