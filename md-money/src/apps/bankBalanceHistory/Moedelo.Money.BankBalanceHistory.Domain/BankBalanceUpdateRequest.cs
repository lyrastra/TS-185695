using System;

namespace Moedelo.Money.BankBalanceHistory.Domain
{
    public class BankBalanceUpdateRequest
    {
        public int SettlementAccountId { get; set; }

        public DateTime BalanceDate { get; set; }

        public decimal StartBalance { get; set; }

        public decimal EndBalance { get; set; }

        public decimal IncomingBalance { get; set; }

        public decimal OutgoingBalance { get; set; }

        public bool IsUserMovement { get; set; }
    }
}
