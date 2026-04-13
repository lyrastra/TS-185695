using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.AdvanceStatements.Models
{
    /// <summary>
    /// Представляет позицию АО
    /// </summary>
    public sealed class AdvanceStatementItemSelfCostDto
    {
        /// <summary>
        /// Идентификатор позиции АО
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор товара/материала
        /// </summary>
        public long StockProductId { get; set; }

        /// <summary>
        /// Кол-во товара/материала/услуг
        /// </summary>
        public decimal Count { get; set; }

        /// <summary>
        /// Сумма с НДС.
        /// </summary>
        public decimal SumWithNds { get; set; }

        /// <summary>
        /// Сумма без НДС.
        /// </summary>
        public decimal SumWithoutNds { get; set; }

        /// <summary>
        /// Ставка НДС
        /// </summary>
        public NdsType NdsType { get; set; }
    }
}