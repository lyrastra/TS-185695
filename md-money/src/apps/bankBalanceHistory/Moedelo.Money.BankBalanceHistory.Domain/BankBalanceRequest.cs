using System;

namespace Moedelo.Money.BankBalanceHistory.Domain
{
    public class BankBalanceRequest
    {
        public int SettlementAccountId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IncludeUserMovement { get; set; }
    }
}
