using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Stocks.Inventory;

namespace Moedelo.StockV2.Dto.Inventories
{
    public class InventoryDto
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        /// <summary> Председатель комиссии </summary>
        public InventoryMemberDto CommissionHead { get; set; }

        /// <summary> Комиссия </summary>
        public List<InventoryMemberDto> CommissionMembers { get; set; }

        /// <summary> МОЛ </summary>
        public List<InventoryMemberDto> ResponsiblePersons { get; set; }

        /// <summary> Причина инвентаризации </summary>
        public InventoryReasonType ReasonType { get; set; }

        /// <summary> Иная причина инвентаризации </summary>
        public string OtherReason { get; set; }

        /// <summary> Номер приказа об инвентаризации </summary>
        public string InventoryOrderNumber { get; set; }

        /// <summary> Дата приказа об инвентаризации </summary>
        public DateTime? InventoryOrderDate { get; set; }

        /// <summary> Результаты инвентаризации </summary>
        public List<InventoryItemDto> Items { get; set; }

        /// <summary> Оприходовать излишки </summary>
        public bool IsDebitToStock { get; set; }

        /// <summary> Учесть в СНО </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        public long DocumentBaseId { get; set; }
        
        public ProvidePostingType TaxPostingType { get; set; }
    }
}