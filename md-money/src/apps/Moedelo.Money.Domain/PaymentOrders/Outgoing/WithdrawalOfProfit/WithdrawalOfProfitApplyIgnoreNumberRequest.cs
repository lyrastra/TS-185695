namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    public class WithdrawalOfProfitApplyIgnoreNumberRequest
    {
        public long[] DocumentBaseIds { get; set; }

        public int ImportRuleId { get; set; }
    }
}
