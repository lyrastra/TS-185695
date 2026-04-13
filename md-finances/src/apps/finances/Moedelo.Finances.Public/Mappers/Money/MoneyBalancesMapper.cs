using System.Collections.Generic;
using System.Linq;
using Moedelo.Finances.Public.ClientData.Money;
using Moedelo.Finances.Public.ResponseData;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;

namespace Moedelo.Finances.Public.Mappers.Money
{
    public static class MoneyBalancesMapper
    {
        public static List<MoneySourceBase> Map(IReadOnlyCollection<MoneySourceDto> sources)
        {
            return sources.Select(x => new MoneySourceBase
            {
                Id = x.Id,
                Type = x.Type
            }).ToList();
        }

        public static List<MoneySourceBalanceDto> Map(IReadOnlyCollection<MoneySourceBalance> balances)
        {
            return balances.Select(x => new MoneySourceBalanceDto
            {
                Id = x.Id,
                Type = x.Type,
                Balance = x.Balance
            }).ToList();
        }
    }
}