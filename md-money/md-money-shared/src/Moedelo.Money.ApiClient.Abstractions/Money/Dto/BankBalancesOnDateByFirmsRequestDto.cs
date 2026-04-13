using System;
using System.Collections.Generic;

namespace Moedelo.Money.ApiClient.Abstractions.Money.Dto
{
    public class BankBalancesOnDateByFirmsRequestDto
    {
        public IReadOnlyCollection<int> FirmIds { get; set; }

        public DateTime OnDate { get; set; }

        public DateTime MinDate { get; set; }
    }
}
