namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks
{
    /// <summary>
    /// Связь ав.сф. с платежом
    /// </summary>
    public class AdvanceInvoiceAndPaymentLink
    {
        public long AdvanceInvoiceBaseId { get; set; }
        
        public long PaymentBaseId { get; set; }
    }
}