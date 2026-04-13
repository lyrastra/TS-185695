namespace Moedelo.Money.BankBalanceHistory.Api.Models
{
    public class LastBankBalanceResponseDto
    {
        public int SettlementAccountId { get; set; }

        public decimal Balance { get; set; }

        public DateTime BalanceDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}
