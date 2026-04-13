using System.Collections.Generic;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost.Inventories
{
    public class InventoryItemSelfCostDto
    {
        public long Id { get; set; }

        /// <summary> Номенклатура (товар/материал) </summary>
        public long StockProductId { get; set; }

        /// <summary> Фактическое кол-во </summary>
        public decimal ActualCount { get; set; }

        /// <summary> Кол-во в учете </summary>
        public decimal RegisteredCount { get; set; }

        /// <summary> Цена </summary>
        public decimal Price { get; set; }

        /// <summary> Является ли излишком </summary>
        public bool IsExcess { get; set; }

        /// <summary>
        /// Является ли недосдачей
        /// </summary>
        public bool IsLoss { get; set; }

        /// <summary> Причины недостачи </summary>
        public IReadOnlyCollection<InventoryLossReasonSelfCostDto> LossReasons { get; set; }
    }
}