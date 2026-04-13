namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks
{
    /// <summary>
    /// Связь счета с платежом
    /// </summary>
    public class BillAndPaymentLink
    {
        public long BillBaseId { get; set; }
        
        public long PaymentBaseId { get; set; }
    }
}