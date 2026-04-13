using System;
using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Money.Operations
{
    public class BalancesRequest
    {
        public DateTime BalancesMasterDate { get; set; }
        public IReadOnlyCollection<MoneySourceBase> MoneySources { get; set; }
        public DateTime OnDate { get; set; }
    }
}
