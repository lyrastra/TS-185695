namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks
{
    /// <summary>
    /// Связь инвойса (покупки) с бюджетным платежом
    /// </summary>
    public class PurchasesCurrencyInvoiceAndBudgetaryPaymentLink
    {
        public long CurrencyInvoiceBaseId { get; set; }
        
        public long PaymentBaseId { get; set; }
    }
}