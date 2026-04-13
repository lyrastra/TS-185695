namespace Moedelo.Money.Reports.DataAccess.Abstractions.Balances.Models
{
    public class SettlementAccountBalanceResponse
    {
        public int FirmId { get; set; }
        public int SettlementAccountId { get; set; }
        public decimal Balance { get; set; }
        public int UnrecognizedIncomingCount { get; set; }
        public decimal UnrecognizedIncomingSum { get; set; }
        public int UnrecognizedOutgoingCount { get; set; }
        public decimal UnrecognizedOutgoingSum { get; set; }
    }
}
