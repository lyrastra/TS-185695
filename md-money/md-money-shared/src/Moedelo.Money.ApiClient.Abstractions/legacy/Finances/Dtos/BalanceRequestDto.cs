using System;
using System.Collections.Generic;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.Finances.Dtos
{
    public class BalanceRequestDto
    {
        public List<MoneySourceDto> Sources { get; set; }

        public DateTime OnDate { get; set; }
    }
}
