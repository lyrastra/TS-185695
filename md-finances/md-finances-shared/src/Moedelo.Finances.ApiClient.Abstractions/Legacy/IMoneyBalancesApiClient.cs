using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy
{
    public interface IMoneyBalancesApiClient
    {
        Task<IReadOnlyList<MoneySourceBalanceDto>> GetBalancesAsync(FirmId firmId, UserId userId,
            BalanceRequestDto request);
        Task ReconcileWithServiceAsync(FirmId firmId, UserId userId, ReconcileRequestDto request);
    }
}