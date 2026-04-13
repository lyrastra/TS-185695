using Moedelo.Infrastructure.AspNetCore.Validation;
using System.Collections.Generic;

namespace Moedelo.Money.BankBalanceHistory.Api.Models
{
    public class BankBalancesOnDateByFirmsRequestDto
    {
        [RequiredValue]
        public IReadOnlyCollection<int> FirmIds { get; set; }

        [DateValue]
        [RequiredValue]
        public DateTime OnDate { get; set; }

        [DateValue]
        [RequiredValue]
        public DateTime MinDate { get; set; }

        public bool IncludeUserMovement { get; set; }
    }
}
