namespace Moedelo.Money.PaymentOrders.Domain.Models
{
    public class CurrencyBalance
    {
        public int SettlementAccountId { get; set; }

        public decimal IncomingSum { get; set; }

        public decimal OutgoingSum { get; set; }
    }
}