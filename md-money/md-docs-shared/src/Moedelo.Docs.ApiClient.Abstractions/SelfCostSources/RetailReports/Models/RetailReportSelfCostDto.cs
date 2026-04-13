using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.RetailReports.Models
{
    /// <summary>
    /// Представляет ОРП с позициями номенклатур для расчёта себестоимости.
    /// </summary>
    public sealed class RetailReportSelfCostDto
    {
        /// <summary>
        /// Идентификатор ОРП.
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер ОРП.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата ОРП.
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Список позиций ОРП.
        /// </summary>
        public IReadOnlyCollection<RetailReportItemSelfCostDto> Items { get; set; }

        /// <summary>
        /// Учитывается в СНО
        /// </summary>
        public TaxationSystemType? TaxationSystem { get; set; }
    }
}