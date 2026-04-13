using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.BankBalanceHistory.Api.Models
{
    public class BankBalanceRequestDto
    {
        [IdIntValue]
        [RequiredValue]
        public int SettlementAccountId { get; set; }

        [DateValue]
        [RequiredValue]
        public DateTime StartDate { get; set; }

        [DateValue]
        [RequiredValue]
        public DateTime EndDate { get; set; }

        public bool IncludeUserMovement { get; set; }
    }
}
