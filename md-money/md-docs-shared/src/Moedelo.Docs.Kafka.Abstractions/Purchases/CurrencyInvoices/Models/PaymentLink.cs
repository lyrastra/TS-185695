namespace Moedelo.Docs.Kafka.Abstractions.Purchases.CurrencyInvoices.Models
{
    public class PaymentLink
    {
        public long DocumentBaseId { get; set; }
        
        public decimal LinkSum { get; set; }
    }
}