namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost
{
    /// <summary>
    /// Представляет позицию требования-накладной
    /// </summary>
    public sealed class RequisitionWaybillItemSelfCostDto
    {
        /// <summary>
        /// Идентификатор позиции требования-накладной
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

        /// <summary>
        /// Сумма товара/материала/услуг
        /// </summary>
        public decimal Sum { get; set; }
    }
}
