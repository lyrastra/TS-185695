using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.ApiClient.Abstractions.legacy.Finances.Dtos.Reconciliation;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.Finances
{
    public interface IMoneyReconciliationClient
    {
        Task<ReconciliationResponseDto[]> GetStatusesAsync(FirmId firmId, UserId userId, ReconciliationStatusRequestDto requestDto);
        Task<ReconciliationResponseDto[]> GetLastWithDiffAsync(FirmId firmId, UserId userId, LastReconciliationWithDiffRequestDto requestDto);
    }
}
