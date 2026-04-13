using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models.Money.Operations;

namespace Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Balances;

public interface IAccMoneyBalancesDao
{
    Task<List<MoneySourceBalance>> GetBySourceIdsAsync(int firmId, BalancesRequest request,
        CancellationToken cancellationToken);
}