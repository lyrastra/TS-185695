namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier
{
    public class PaymentToSupplierApplyIgnoreNumberRequest
    {
        public long[] DocumentBaseIds { get; set; }

        public int ImportRuleId { get; set; }
    }
}
