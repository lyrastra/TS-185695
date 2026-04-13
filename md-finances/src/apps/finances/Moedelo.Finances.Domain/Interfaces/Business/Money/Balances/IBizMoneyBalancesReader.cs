using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.Balances
{
    public interface IBizMoneyBalancesReader : IDI
    {
        Task<List<MoneySourceBalance>> GetAsync(IUserContext userContext, IReadOnlyCollection<MoneySourceBase> moneySources, DateTime? date = null);

        Task<decimal> GetBalancesSumAsync(IUserContext userContext, MoneySourceType sourceType, long? sourceId);
    }
}
