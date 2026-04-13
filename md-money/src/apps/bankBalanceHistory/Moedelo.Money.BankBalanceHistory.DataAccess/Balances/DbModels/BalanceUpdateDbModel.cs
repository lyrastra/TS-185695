using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moedelo.Money.BankBalanceHistory.DataAccess.Balances.DbModels
{
    public class BalanceUpdateDbModel
    {
        [Column("FirmId")]
        public int FirmId { get; set; }

        [Column("SettlementAccountId")]
        public int SettlementAccountId { get; set; }

        [Column("BalanceDate")]
        public DateTime BalanceDate { get; set; }

        [Column("StartBalance")]
        public decimal StartBalance { get; set; }

        [Column("EndBalance")]
        public decimal EndBalance { get; set; }

        [Column("IncomingBalance")]
        public decimal IncomingBalance { get; set; }

        [Column("OutgoingBalance")]
        public decimal OutgoingBalance { get; set; }

        [Column("IsUserMovement")]
        public bool IsUserMovement { get; set; }
    }
}
