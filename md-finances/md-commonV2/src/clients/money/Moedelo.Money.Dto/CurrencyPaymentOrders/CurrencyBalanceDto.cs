namespace Moedelo.Money.Dto.CurrencyPaymentOrders
{
    public class CurrencyBalanceDto
    {
        public int SettlementAccountId { get; set; }
        
        public decimal IncomingSum { get; set; }
        
        public decimal OutgoingSum { get; set; }
    }
}