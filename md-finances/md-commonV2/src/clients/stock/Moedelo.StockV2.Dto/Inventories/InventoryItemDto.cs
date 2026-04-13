using System.Collections.Generic;

namespace Moedelo.StockV2.Dto.Inventories
{
    public class InventoryItemDto
    {
        public long Id { get; set; }

        /// <summary> Номенклатура (товар/материал) </summary>
        public long StockProductId { get; set; }

        /// <summary> Склад </summary>
        public long StockId { get; set; }

        /// <summary> Фактическое кол-во </summary>
        public decimal ActualCount { get; set; }

        /// <summary> Кол-во в учете </summary>
        public decimal RegisteredCount { get; set; }

        /// <summary> Цена </summary>
        public decimal Price { get; set; }

        /// <summary> Причины недостачи </summary>
        public List<InventoryLossReasonDto> LossReasons { get; set; }

        /// <summary> Является ли излишком </summary>
        public bool IsExcess { get; set; }

        /// <summary>
        /// Является ли недосдачей
        /// </summary>
        public bool IsLoss { get; set; }

        /// <summary>
        /// Является ли готовой продукцией
        /// </summary>
        public bool IsManufactured { get; set; }

        /// <summary>
        /// Является ли материалом
        /// </summary>
        public bool IsMaterial { get; set; }
    }
}