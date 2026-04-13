namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks
{
    /// <summary>
    /// Связь платежа с АО
    /// </summary>
    public class PaymentAndAdvanceStatementLink
    {
        public long PaymentBaseId { get; set; }
        
        public long AdvanceStatementBaseId { get; set; }
    }
}