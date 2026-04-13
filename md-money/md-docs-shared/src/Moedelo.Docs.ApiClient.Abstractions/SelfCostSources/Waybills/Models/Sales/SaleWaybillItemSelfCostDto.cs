using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Waybills.Models.Sales
{
    /// <summary>
    /// Представляет позицию накладной продажи.
    /// </summary>
    public class SaleWaybillItemSelfCostDto
    {
        /// <summary>
        /// Идентификатор позиции накладной.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор товара/материала.
        /// </summary>
        public long? StockProductId { get; set; }

        /// <summary>
        /// Кол-во товара/материала.
        /// </summary>
        public decimal ItemCount { get; set; }

        /// <summary>
        /// Сумма с НДС.
        /// </summary>
        public decimal SumWithoutNds { get; set; }

        /// <summary>
        /// Сумма НДС.
        /// </summary>
        public decimal NdsSum { get; set; }

        /// <summary>
        /// Тип НДС.
        /// </summary>
        public NdsType NdsType { get; set; }

        /// <summary>
        /// Сумма с НДС.
        /// </summary>
        public decimal SumWithNds { get; set; }
    }
}