using System;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto
{
    public class ReconcileRequestDto
    {
        public MoneySourceBalanceDto[] Balances { get; set; }

        public DateTime OnDate { get; set; }
    }
}