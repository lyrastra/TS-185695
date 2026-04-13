namespace Moedelo.Finances.Domain.Models.AccountBalances
{
    public class FirmSettlementAccountBalance
    {
        public int FirmId { get; set; }

        public string FirmName { get; set; }

        public int SettlementAccountId { get; set; }

        public string SettlementAccountName { get; set; }

        public string SettlementAccountNumber { get; set; }

        public int SettlementAccountBankId { get; set; }
        
        public string SettlementAccountBankName { get; set; }
        public decimal Balance { get; set; }
    }
}