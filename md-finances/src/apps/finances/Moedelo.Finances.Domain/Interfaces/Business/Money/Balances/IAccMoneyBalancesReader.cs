using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.Balances
{
    public interface IAccMoneyBalancesReader
    {
        Task<List<MoneySourceBalance>> GetAsync(int firmId, int userId,
            IReadOnlyCollection<MoneySourceBase> moneySources, DateTime? date = null,
            CancellationToken cancellationToken = default);

        Task<decimal> GetBalancesSumAsync(int firmId, int userId, MoneySourceType sourceType, long? sourceId);

        Task<Dictionary<Currency, decimal>> GetCurrencyBalancesSumAsync(int firmId, int userId, MoneySourceType sourceType, long? sourceId);
    }
}
