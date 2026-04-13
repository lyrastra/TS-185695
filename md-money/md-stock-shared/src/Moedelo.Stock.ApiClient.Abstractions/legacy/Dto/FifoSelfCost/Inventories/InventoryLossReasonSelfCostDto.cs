using Moedelo.Stock.Enums;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost.Inventories
{
    public class InventoryLossReasonSelfCostDto
    {
        /// <summary> Причина недостачи </summary>
        public LossReasonType Type { get; set; }

        /// <summary> Кол-во недостающей номенклатуры </summary>
        public decimal Count { get; set; }
    }
}