using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Waybills.Models.Sales
{
    /// <summary>
    /// Представляет Накладную продажи с позициями номенклатур для расчёта себестоимости.
    /// </summary>
    public class SaleWaybillSelfCostDto
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
        public IReadOnlyList<SaleWaybillItemSelfCostDto> Items { get; set; }

        /// <summary>
        /// Идентификатор контрагента.
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Учитывается в СНО.
        /// </summary>
        public TaxationSystemType? TaxationSystem { get; set; }

        /// <summary>
        /// Тип накладной
        /// </summary>
        public WaybillTypeCode Type { get; set; }
    }
}