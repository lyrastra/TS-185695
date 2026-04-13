using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Waybills.Models.Purchases
{
    /// <summary>
    /// Представляет Накладную покупки с позициями номенклатур для расчёта себестоимости.
    /// </summary>
    public class PurchaseWaybillSelfCostDto
    {
        /// <summary>
        /// Идентификатор накладной.
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер накладной.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата накладной.
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Список позиций накладной.
        /// </summary>
        public IReadOnlyList<PurchaseWaybillItemSelfCostDto> Items { get; set; }

        /// <summary>
        /// Тип накладной.
        /// </summary>
        public WaybillTypeCode Type { get; set; }

        /// <summary>
        /// Сумма накладной.
        /// </summary>
        public decimal DocumentSum { get; set; }
    }
}