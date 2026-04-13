using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.PaymentHistory;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Abstractions.PaymentHistory;

public interface IPaymentHistoryV3ApiClient
{
    Task<List<PaymentHistoryDto>> GetByCriteriaAsync(PaymentHistoryRequestDto criteria,
        HttpQuerySetting setting = null);

    Task<PaymentHistoryDto> GetByIdAsync(int paymentHistoryId);
    Task<List<PaymentHistoryPositionDto>> GetPositionsByIdAsync(int paymentHistoryId);
    Task<Dictionary<int, List<PaymentHistoryPositionDto>>> GetPaymentHistoriesPositionsAsync(
        IReadOnlyCollection<int> paymentHistoryIds);
    Task<List<int>> GetPaidFirmIdsByCriteriaAsync(PaymentHistoryPaidFirmIdsRequestDto criteria);
}
