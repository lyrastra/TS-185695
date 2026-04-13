using System;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models
{
    /// <summary>
    /// Упрощённая модель отчета комиссионера для приватного api
    /// </summary>
    public class CommissionAgentReportPrivateDto
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
        /// Склад
        /// </summary>
        public long? StockId { get; set; }

        /// <summary>
        /// Сумма комиссии
        /// </summary>
        public decimal CommissionSum { get; set; }
    }
}
