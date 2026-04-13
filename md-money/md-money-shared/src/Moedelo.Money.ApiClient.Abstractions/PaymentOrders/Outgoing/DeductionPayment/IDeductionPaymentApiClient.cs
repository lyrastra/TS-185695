using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.DeductionPayment.Models;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.DeductionPayment
{
    public interface IDeductionPaymentApiClient
    {
        Task<PaymentOrderSaveResponseDto> CreateAsync(DeductionPaymentSaveDto dto);

        Task DeleteByDocumentBaseIdAsync(long documentBaseId);
    }
}