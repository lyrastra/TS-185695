using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Upds.Models.Purchases
{
    /// <summary>
    /// Представляет позицию УПД покупки.
    /// </summary>
    public sealed class PurchaseUpdItemSelfCostDto
    {
        /// <summary>
        /// Идентификатор позиции УПД
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Тип НДС
        /// </summary>
        public NdsType NdsType { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal NdsSum { get; set; }

        /// <summary>
        /// Сумма с НДС
        /// </summary>
        public decimal SumWithNds { get; set; }

        /// <summary>
        /// Сумма без НДС
        /// </summary>
        public decimal SumWithoutNds { get; set; }

        /// <summary>
        /// Кол-во товара/материала/услуг
        /// </summary>
        public decimal ItemCount { get; set; }

        /// <summary>
        /// Идентификатор товара/материала.
        /// </summary>
        public long? StockProductId { get; set; }
    }
}