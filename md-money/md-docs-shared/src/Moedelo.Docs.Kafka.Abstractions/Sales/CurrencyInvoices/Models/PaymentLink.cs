namespace Moedelo.Docs.Kafka.Abstractions.Sales.CurrencyInvoices.Models
{
    public class PaymentLink
    {
        public long DocumentBaseId { get; set; }
        
        public decimal LinkSum { get; set; }
    }
}