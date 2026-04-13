using System;
using System.Collections.Generic;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost.Inventories
{
    public class InventoryFifoSelfCostSourceDto
    {
        public long DocumentBaseId { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DocumentDate { get; set; }

        /// <summary> Результаты инвентаризации </summary>
        public IReadOnlyCollection<InventoryItemSelfCostDto> Items { get; set; }

        /// <summary> Учесть в СНО </summary>
        public int? TaxationSystemType { get; set; }
    }
}