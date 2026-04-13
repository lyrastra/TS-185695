using System;

namespace Moedelo.Money.BankBalanceHistory.Business.Abstractions.Events
{
    public class MovementProcessedEvent
    {
        public int FirmId { get; set; }
        public int SettlementAccountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
