using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.RetailRefunds.Models
{
    /// <summary>
    /// Представляет розничный возврат от покупателя с позициями номенклатур для расчёта себестоимости.
    /// </summary>
    public sealed class RetailRefundSelfCostDto
    {
        public long DocumentBaseId { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DocumentDate { get; set; }

        public IReadOnlyCollection<RetailRefundItemSelfCostDto> Items { get; set; }

        public TaxationSystemType? TaxationSystem { get; set; }

        /// <summary>
        /// Идентификатор ОРП, связанного с возвратом.
        /// </summary>
        public long RetailReportId { get; set; }
    }
}