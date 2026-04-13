using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.Billing;
using Moedelo.BillingV2.Dto.Billing.PaymentPositions;
using Moedelo.BillingV2.Dto.PaymentHistory;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.BillingV2.Client.PaymentHistory
{
    public interface IPaymentHistoryApiClient : IDI
    {
        Task<PaymentHistoryDto> GetAsync(int id);
        Task<List<PaymentHistoryDto>> GetAsync(IReadOnlyCollection<int> ids);
        Task<List<PaymentHistoryDto>> GetAsync(PaymentHistoryRequestDto criteria);
        Task MarkAsExportedTo1cAsync(PaymentHistoryMarkAsExportedTo1cRequestDto requestDto, HttpQuerySetting setting = null);
        Task<List<PaymentPositionDto>> GetPositionsAsync(int paymentHistoryId);
        Task<Dictionary<int, List<PaymentPositionDto>>> GetPositionsAsync(IReadOnlyCollection<int> paymentHistoryIds);
        Task UpdatePositionsAsync(int paymentHistoryId, IReadOnlyCollection<PaymentPositionDto> positionDtos);
        Task<IReadOnlyDictionary<int, List<PaymentHistoryDto>>> GetActiveOrLastWithAccessAsync(IReadOnlyCollection<int> firmIds);
        Task UpdateValidityPeriodAsync(UpdateValidityPeriodRequestDto requestDto);
        Task UpdatePositionsAndUpdateValidityPeriodAsync(PositionsAndValidityPeriodRequestDto requestDto);
        Task UpdateIncomingDateAsync(UpdateIncomingDateRequestDto requestDto);
    }
}