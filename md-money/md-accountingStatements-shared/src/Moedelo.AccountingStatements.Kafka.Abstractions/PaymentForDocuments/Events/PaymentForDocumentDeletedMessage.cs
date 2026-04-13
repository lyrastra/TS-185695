namespace Moedelo.AccountingStatements.Kafka.Abstractions.PaymentForDocuments.Events
{
    /// <summary>
    /// Событие по удалению бухсправки с типом "Признание предоплаты оплатой"
    /// </summary>
    public class PaymentForDocumentDeletedMessage
    {
        /// <summary>
        /// DocumentBaseId бухсправки
        /// </summary>
        public long DocumentBaseId { get; set; }
    }
}