namespace Moedelo.AgentsV2.Dto.WithdrawalMoney
{
    public class RequestForWithdrawalOnYandexMoneyWalletDto
    {
        public long Id { get; set; }

        public int PartnerId { get; set; }

        public string Login { get; set; }

        public decimal Amount { get; set; }

        public string PersonalWalletNumber { get; set; }

        public string WalletType { get; set; }
    }
}
