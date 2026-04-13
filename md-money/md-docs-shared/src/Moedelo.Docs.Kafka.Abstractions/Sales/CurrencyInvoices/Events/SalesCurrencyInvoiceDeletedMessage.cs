namespace Moedelo.Docs.Kafka.Abstractions.Sales.CurrencyInvoices.Events
{
    public class SalesCurrencyInvoiceDeletedMessage
    {
        public long DocumentBaseId { get; set; }

        public int KontragentId { get; set; }
    }
}