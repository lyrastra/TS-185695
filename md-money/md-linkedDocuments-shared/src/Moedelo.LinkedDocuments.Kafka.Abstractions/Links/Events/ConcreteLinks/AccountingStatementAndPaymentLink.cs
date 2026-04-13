namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks
{
    /// <summary>
    /// Связь бухсправки с платежом
    /// </summary>
    public class AccountingStatementAndPaymentLink
    {
        public long AccountingStatementBaseId { get; set; }
        
        public long PaymentBaseId { get; set; }
    }
}