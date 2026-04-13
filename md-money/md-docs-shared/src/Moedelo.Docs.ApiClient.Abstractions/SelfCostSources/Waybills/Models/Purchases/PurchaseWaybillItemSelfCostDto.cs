using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Waybills.Models.Purchases
{
    /// <summary>
    /// Представляет позицию накладной покупки.
    /// </summary>
    public class PurchaseWaybillItemSelfCostDto
    {
        /// <summary>
        /// Идентификатор позиции накладной.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Тип НДС.
        /// </summary>
        public NdsType NdsType { get; set; }

        /// <summary>
        /// Сумма НДС.
        /// </summary>
        public decimal NdsSum { get; set; }

        /// <summary>
        /// Сумма с НДС.
        /// </summary>
        public decimal SumWithNds { get; set; }

        /// <summary>
        /// Сумма без НДС.
        /// </summary>
        public decimal SumWithoutNds { get; set; }

        /// <summary>
        /// Кол-во товара/материала.
        /// </summary>
        public decimal ItemCount { get; set; }

        /// <summary>
        /// Идентификатор товара/материала.
        /// </summary>
        public long? StockProductId { get; set; }

        /// <summary>
        /// Излишек кол-ва при несоответствии кол-ву/качеству
        /// </summary>
        public decimal DiscrepancyOverCount { get; set; }
    }
}