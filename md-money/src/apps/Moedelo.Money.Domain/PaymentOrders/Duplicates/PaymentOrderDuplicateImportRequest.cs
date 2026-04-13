namespace Moedelo.Money.Domain.PaymentOrders.Duplicates
{
    public class PaymentOrderDuplicateImportRequest
    {
        public long DuplicateBaseId { get; set; }
        public long DocumentBaseId { get; set; }
    }
}