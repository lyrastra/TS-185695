namespace Moedelo.Money.ApiClient.Abstractions.Money.Dto
{
    public class BankBalanceResponseDto
    {
        public decimal StartBalance { get; set; }

        public decimal EndBalance { get; set; }

        public decimal IncomingBalance { get; set; }

        public decimal OutgoingBalance { get; set; }
    }
}
