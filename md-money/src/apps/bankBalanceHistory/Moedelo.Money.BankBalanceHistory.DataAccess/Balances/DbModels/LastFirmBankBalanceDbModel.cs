using System;

namespace Moedelo.Money.BankBalanceHistory.DataAccess.Balances.DbModels
{
    public class LastFirmBankBalanceDbModel
    {
        public int FirmId { get; set; }

        public int SettlementAccountId { get; set; }

        public decimal Balance { get; set; }

        public DateTime BalanceDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}
