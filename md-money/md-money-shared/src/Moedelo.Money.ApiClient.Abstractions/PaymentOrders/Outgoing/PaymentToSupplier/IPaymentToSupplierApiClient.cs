using System.Threading;
using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Dto;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierApiClient
    {
        Task<PaymentOrderSaveResponseDto> CreateAsync(PaymentToSupplierSaveDto dto);

        Task DeleteAsync(int companyId, long paymentBaseId, CancellationToken ct);
    }
}