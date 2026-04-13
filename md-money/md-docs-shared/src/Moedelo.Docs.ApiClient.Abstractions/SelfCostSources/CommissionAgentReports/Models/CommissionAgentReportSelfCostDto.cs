using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CommissionAgentReports.Models
{
    /// <summary>
    /// Представляет отчет комиссионера с позициями номенклатур для расчёта себестоимости.
    /// </summary>
    public sealed class CommissionAgentReportSelfCostDto
    {
        /// <summary>
        /// Идентификатор отчета комиссионера.
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер отчета комиссионера.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата отчета комиссионера.
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Список позиций отчета комиссионера.
        /// </summary>
        public IReadOnlyCollection<CommissionAgentReportItemSelfCostDto> Items { get; set; }
    }
}