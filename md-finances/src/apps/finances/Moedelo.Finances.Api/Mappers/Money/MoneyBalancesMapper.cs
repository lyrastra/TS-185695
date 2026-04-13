using System.Collections.Generic;
using System.Linq;
using Moedelo.Finances.Client.Money.Dto;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;

namespace Moedelo.Finances.Api.Mappers.Money
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

        public static List<MoneySourceBalance> Map(IReadOnlyCollection<MoneySourceBalanceDto> balances)
        {
            return balances.Select(x => new MoneySourceBalance
            {
                Id = x.Id,
                Type = x.Type,
                Balance = x.Balance
            }).ToList();
        }
    }
}