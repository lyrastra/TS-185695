using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.PaymentHistorySellers;

namespace Moedelo.Billing.Abstractions.Legacy.Interfaces.PaymentHistorySellers;

public interface IPaymentHistorySellersApiClient
{
    Task<PaymentHistorySellersValidationResponseDto> ValidateAsync(PaymentHistorySellersUpdateRequestDto dto);
    Task SaveAsync(PaymentHistorySellersUpdateRequestDto dto);
    Task<PaymentHistorySellerDto[]> GetAsync(int paymentHistoryId);
}