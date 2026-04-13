using System;
using System.Collections.Generic;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto
{
    public class BalanceRequestDto
    {
        public IReadOnlyList<MoneySourceDto> Sources { get; set; }

        public DateTime OnDate { get; set; }
    }
}