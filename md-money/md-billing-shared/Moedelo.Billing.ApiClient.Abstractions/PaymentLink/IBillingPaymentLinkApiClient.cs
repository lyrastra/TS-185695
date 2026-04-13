using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.PaymentLink.Dto;

namespace Moedelo.Billing.Abstractions.PaymentLink
{
    public interface IBillingPaymentLinkApiClient
    {
        Task<PaymentLinkDto> GetPaymentLinkInfoByGuidAsync(string linkId);
        Task<PaymentLinkDto> GetPaymentLinkInfoByPaymentHistoryIdAsync(int paymentHistoryId);
        Task<List<PaymentLinkDto>> GetPaymentLinkInfoByPaymentHistoryIdsAsync(
            IReadOnlyCollection<int> paymentHistoryIds);
        Task CreateNewGuidEntryAsync(PaymentLinkRequestDto request);
    }
}
