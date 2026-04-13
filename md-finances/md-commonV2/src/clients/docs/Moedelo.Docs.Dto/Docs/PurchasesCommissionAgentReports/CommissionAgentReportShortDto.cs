using System;

namespace Moedelo.Docs.Dto.Docs.PurchasesCommissionAgentReports
{
    /// <summary>
    /// Упрощённая модель отчета комиссионера
    /// </summary>
    public class CommissionAgentReportShortDto
    {
        /// <summary>
        /// Идентификатор отчета
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер отчета
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата отчета
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Идентификатор комиссионера
        /// </summary>
        public int CommissionAgentId { get; set; }

        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Склад
        /// </summary>
        public long? StockId { get; set; }
    }
}