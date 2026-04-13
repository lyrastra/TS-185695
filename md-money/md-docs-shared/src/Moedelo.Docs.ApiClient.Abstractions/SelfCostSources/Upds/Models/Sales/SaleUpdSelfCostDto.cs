using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Upds.Models.Sales
{
    /// <summary>
    /// Представляет УПД продажи с позициями номенклатур для расчёта себестоимости.
    /// </summary>
    public sealed class SaleUpdSelfCostDto
    {
        /// <summary>
        /// Идентификатор УПД.
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер УПД.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата УПД.
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Список позиций УПД.
        /// </summary>
        public IReadOnlyList<SaleUpdItemSelfCostDto> Items { get; set; }

        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Учитывается в СНО
        /// </summary>
        public TaxationSystemType? TaxationSystem { get; set; }
    }
}