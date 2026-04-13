using System;

namespace Moedelo.Money.BankBalanceHistory.Dao.Abstractions.Models
{
    public class BalanceUpdateModel
    {
        public int FirmId { get; set; }
        public int SettlementAccountId { get; set; }
        public DateTime BalanceDate { get; set; }
        public decimal StartBalance { get; set; }
        public decimal EndBalance { get; set; }
        public decimal IncomigBalance { get; set; }
        public decimal OutgoingBalance { get; set; }
    }
}
