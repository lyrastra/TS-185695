namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.RetailRefunds.Models
{
    /// <summary>
    /// Представляет позицию розничного возвратв от покупателя
    /// </summary>
    public sealed class RetailRefundItemSelfCostDto
    {
        /// <summary>
        /// Идентификатор позиции ОРП
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор товара/материала
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Кол-во товара/материала/услуг
        /// </summary>
        public decimal Count { get; set; }
    }
}