namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks
{
    /// <summary>
    /// Связь валютного платежа поставщику с инвойсом (покупки)
    /// </summary>
    public class CurrencyPaymentToSupplierAndCurrencyInvoiceLink
    {
        public long CurrencyInvoiceBaseId { get; set; }
        
        public long PaymentBaseId { get; set; }
    }
}