using Moedelo.Money.BankBalanceHistory.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances
{
    public interface IBalancesReader
    {
        Task<BankBalanceResponse> GetAsync(BankBalanceRequest request);

        Task<LastBankBalance[]> GetOnDateByFirmIdAsync(DateTime onDate, bool includeUserMovement, DateTime? minDate = null);

        Task<IReadOnlyDictionary<int, LastBankBalance[]>> GetOnDateByFirmIdsAsync(IReadOnlyCollection<int> firmIds, DateTime onDate, DateTime minDate, bool includeUserMovement);
    }
}