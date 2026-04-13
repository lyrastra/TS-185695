using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Legacy.Dto;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Abstractions.Legacy.Interfaces;

public interface IPaymentHistoryApiClient
{
    Task<List<PaymentHistoryDto>> GetPaymentHistoryForFirmAsync(int firmId);
    Task<int> SavePaymentHistoryAsync(PaymentHistoryDto paymentHistory);
    Task<List<PaymentHistoryDto>> GetByCriteriaAsync(PaymentHistoryRequestDto paymentHistoryRequestDto);
    Task<FirmPackagesInfoDto[]> GetFirmsPaidPackages(IReadOnlyCollection<int> firmIds);
    Task<List<PaymentPositionDto>> GetPositionsAsync(int paymentHistoryId);
    Task<Dictionary<int, List<PaymentPositionDto>>> GetPositionsAsync(IReadOnlyCollection<int> paymentHistoryIds);
    Task UpdateIncomingDateAsync(UpdateIncomingDateRequestDto requestDto);
    Task<PaymentHistoryDto> GetAsync(int paymentHistoryId);

    Task MarkAsExportedTo1CAsync(PaymentHistoryMarkAsExportedTo1CRequestDto requestDto,
        HttpQuerySetting setting = null);
}