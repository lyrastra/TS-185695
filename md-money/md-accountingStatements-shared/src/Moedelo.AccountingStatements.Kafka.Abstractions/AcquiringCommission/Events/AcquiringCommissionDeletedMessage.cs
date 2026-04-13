namespace Moedelo.AccountingStatements.Kafka.Abstractions.AcquiringCommission.Events
{
    /// <summary>
    /// Событие по удалению бухсправки с типом "Комиссия за эквайринг"
    /// </summary>
    public class AcquiringCommissionDeletedMessage
    {
        /// <summary>
        /// DocumentBaseId бухсправки
        /// </summary>
        public long DocumentBaseId { get; set; }
    }
}