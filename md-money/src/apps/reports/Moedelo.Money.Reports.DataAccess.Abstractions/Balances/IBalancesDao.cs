using Moedelo.Money.Reports.DataAccess.Abstractions.Balances.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Reports.DataAccess.Abstractions.Balances
{
    public interface IBalancesDao
    {
        Task<IReadOnlyList<SettlementAccountBalanceResponse>> GetAsync(SettlementAccountBalancesRequest request);
    }
}
