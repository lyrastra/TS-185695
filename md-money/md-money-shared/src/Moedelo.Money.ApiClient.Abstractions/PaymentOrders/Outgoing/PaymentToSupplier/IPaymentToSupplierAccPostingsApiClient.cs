using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto.Providing.AccPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Dto;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierAccPostingsApiClient
    {
        Task<AccPostingsResponseDto> GenerateAsync(PaymentToSupplierAccPostingsGenerateRequestDto generateRequest);
    }
}
