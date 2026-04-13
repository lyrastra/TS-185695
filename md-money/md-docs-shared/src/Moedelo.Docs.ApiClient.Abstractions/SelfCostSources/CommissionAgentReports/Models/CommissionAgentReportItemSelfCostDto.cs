using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CommissionAgentReports.Models
{
    /// <summary>
    /// Представляет позицию отчета комиссионера
    /// </summary>
    public sealed class CommissionAgentReportItemSelfCostDto
    {
        /// <summary>
        /// Идентификатор позиции отчета комиссионера
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор товара/материала
        /// </summary>
        public long StockProductId { get; set; }

        /// <summary>
        /// Кол-во товара/материала/услуг
        /// </summary>
        public decimal Count { get; set; }

        /// <summary>
        /// Дата отгрузки/дата возврата
        /// </summary>
        public DateTime? ShipmentDate { get; set; }

        /// <summary>
        /// Идентификатор отчёта комиссионера, по которому возврат
        /// </summary>
        public long? RefundCommissionAgentReportId { get; set; }
        
        /// <summary>
        /// Признак, указывающий учитывается ли данная позиция
        /// </summary>
        public bool Unaccounted { get; set; }
    }
}