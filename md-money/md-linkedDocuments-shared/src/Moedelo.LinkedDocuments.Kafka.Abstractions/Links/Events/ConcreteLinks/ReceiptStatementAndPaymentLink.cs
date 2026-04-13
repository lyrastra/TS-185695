namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks
{
    /// <summary>
    /// Связь акта приема-передачи с платежом
    /// </summary>
    public class ReceiptStatementAndPaymentLink
    {
        public long ReceiptStatementBaseId { get; set; }

        public long PaymentBaseId { get; set; }
    }
}
