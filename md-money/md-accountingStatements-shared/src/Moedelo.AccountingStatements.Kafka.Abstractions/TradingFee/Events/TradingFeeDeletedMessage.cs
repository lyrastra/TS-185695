namespace Moedelo.AccountingStatements.Kafka.Abstractions.TradingFee.Events
{
    /// <summary>
    /// Событие по удалению бухсправки с типом "Торговый сбор"
    /// </summary>
    public class TradingFeeDeletedMessage
    {
        /// <summary>
        /// DocumentBaseId бухсправки
        /// </summary>
        public long DocumentBaseId { get; set; }
    }
}