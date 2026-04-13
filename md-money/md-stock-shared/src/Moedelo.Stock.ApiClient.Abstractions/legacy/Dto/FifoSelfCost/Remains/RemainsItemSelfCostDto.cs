namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost
{
    /// <summary>
    /// Модель позиции складской операции ввода остатков
    /// </summary>
    public class RemainsItemSelfCostDto
    {
        /// <summary>
        /// Идентификатор позиции остатков
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Кол-во остатка товара/материала
        /// </summary>
        public decimal ItemCount { get; set; }

        /// <summary>
        /// Идентификатор товара/материала.
        /// </summary>
        public long StockProductId { get; set; }

        /// <summary>
        /// Сумма остатка товара/материала
        /// </summary>
        public decimal Sum { get; set; }
    }
}