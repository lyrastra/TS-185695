namespace Moedelo.Money.BankBalanceHistory.Domain
{
    public class BankBalanceResponse
    {
        public decimal StartBalance { get; set; }

        public decimal EndBalance { get; set; }

        public decimal IncomingBalance { get; set; }

        public decimal OutgoingBalance { get; set; }
    }
}
