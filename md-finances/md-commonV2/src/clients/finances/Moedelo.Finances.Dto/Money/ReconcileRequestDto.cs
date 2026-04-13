using System;
using System.Collections.Generic;

namespace Moedelo.Finances.Client.Money.Dto
{
    public class ReconcileRequestDto
    {
        public List<MoneySourceBalanceDto> Balances { get; set; }
        public DateTime OnDate { get; set; }
    }
}
