using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Balances;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.Finances.Business.Services.Money.Balances
{
    [InjectAsSingleton]
    public class BizMoneyBalancesReader : IBizMoneyBalancesReader
    {
        public BizMoneyBalancesReader()
        {
        }

        public Task<List<MoneySourceBalance>> GetAsync(IUserContext userContext, IReadOnlyCollection<MoneySourceBase> moneySources, DateTime? date = null)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetBalancesSumAsync(IUserContext userContext, MoneySourceType sourceType, long? sourceId)
        {
            throw new NotImplementedException();
        }
    }
}