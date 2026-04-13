using System;
using System.Collections.Generic;

namespace Moedelo.Finances.Client.Money.Dto
{
    public class BalanceRequestDto
    {
        public List<MoneySourceDto> Sources { get; set; }

        public DateTime OnDate { get; set; }
    }
}
