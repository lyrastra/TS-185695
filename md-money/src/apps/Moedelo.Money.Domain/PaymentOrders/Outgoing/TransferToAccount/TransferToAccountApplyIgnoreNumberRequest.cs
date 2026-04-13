namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount
{
    public class TransferToAccountApplyIgnoreNumberRequest
    {
        public long[] DocumentBaseIds { get; set; }

        public int ImportRuleId { get; set; }
    }
}
