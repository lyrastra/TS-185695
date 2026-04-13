using Moedelo.Money.BankBalanceHistory.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.BankBalanceHistory.DataAccess.Abstractions
{
    public interface IBalancesDao
    {
        Task<bool> IsContainsDataForPeriodAsync(int firmId, int settlementAccountId, DateTime startDate, DateTime endDate, bool includeUserMovement);

        Task<BankBalanceResponse> GetAsync(int firmId, BankBalanceRequest request);

        Task<LastBankBalance[]> GetOnDateByFirmIdAsync(int firmId, DateTime onDate, DateTime minDate, bool includeUserMovement);

        Task<IReadOnlyDictionary<int, LastBankBalance[]>> GetOnDateByFirmIdsAsync(IReadOnlyCollection<int> firmIds, DateTime onDate, DateTime minDate, bool includeUserMovement);

        Task UpdateAsync(int firmId, IReadOnlyCollection<BankBalanceUpdateRequest> requests);

        Task CleanUpByFirmIdsAsync(IReadOnlyCollection<int> firmIds);
    }
}
