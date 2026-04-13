namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.ProductIncomes.Models
{
    /// <summary>
    /// Представляет позицию прихода без документов
    /// </summary>
    public class ProductIncomeItemSelfCostDto
    {
        /// <summary>
        /// Идентификатор позиции прихода без документов
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор товара/материала
        /// </summary>
        public long StockProductId { get; set; }

        /// <summary>
        /// Кол-во товара/материала
        /// </summary>
        public decimal Count { get; set; }
    }
}