using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.BankBalanceHistory.Api.Models
{
    public class BalanceRequestDto
    {
        [IdIntValue]
        [RequiredValue]
        public int SettlementAccountId { get; set; }

        [DateValue]
        [RequiredValue]
        public DateTime BalanceDate { get; set; }

        [RequiredValue]
        public decimal StartBalance { get; set; }

        [RequiredValue]
        public decimal EndBalance { get; set; }

        [RequiredValue]
        public decimal IncomingBalance { get; set; }

        [RequiredValue]
        public decimal OutgoingBalance { get; set; }

        public bool IsUserMovement { get; set; }
    }
}