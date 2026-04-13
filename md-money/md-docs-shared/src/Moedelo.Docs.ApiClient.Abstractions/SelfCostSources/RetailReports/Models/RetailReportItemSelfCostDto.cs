namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.RetailReports.Models
{
    /// <summary>
    /// Представляет позицию ОРП
    /// </summary>
    public sealed class RetailReportItemSelfCostDto
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