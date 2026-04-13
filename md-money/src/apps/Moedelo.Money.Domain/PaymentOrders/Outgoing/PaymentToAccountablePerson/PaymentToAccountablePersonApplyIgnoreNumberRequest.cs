namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public class PaymentToAccountablePersonApplyIgnoreNumberRequest
    {
        public long[] DocumentBaseIds { get; set; }

        public int ImportRuleId { get; set; }
    }
}
