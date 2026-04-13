namespace Moedelo.Docs.Kafka.Abstractions.Purchases.CurrencyInvoices.Messages
{
    public class PurchaseCurrencyInvoiceDeletedMessage
    {
        public long DocumentBaseId { get; set; }
        
        public long[] CurrencyPaymentsToSupplierIds { get; set; }

        public int KontragentId { get; set; }
    }
}