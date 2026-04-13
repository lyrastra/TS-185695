namespace Moedelo.AgentsV2.Dto.WithdrawalMoney
{
    public class RequestForWithdrawalOnSettlementAccountDto
    {
        public long Id { get; set; }

        public int PartnerId { get; set; }

        public string Login { get; set; }

        public decimal Amount { get; set; }

        public string SettlementAccount { get; set; }

        public int BankId { get; set; }

        public string BankName { get; set; }

        public string BankBik { get; set; }

        public string CorrespondentAccount { get; set; }
    }
}
