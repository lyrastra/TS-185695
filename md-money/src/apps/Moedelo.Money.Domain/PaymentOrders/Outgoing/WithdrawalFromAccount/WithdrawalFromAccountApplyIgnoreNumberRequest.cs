namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    public class WithdrawalFromAccountApplyIgnoreNumberRequest
    {
        public long[] DocumentBaseIds { get; set; }

        public int ImportRuleId { get; set; }
    }
}
