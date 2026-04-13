using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models
{
    /// <summary>
    /// отчёт посредника (коммисионера) который можно указать в возвратах
    /// </summary>
    public class CommissionAgentReportForRefundsResponseDto
    {
        /// <summary>
        /// Идентификатор отчета
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Дата отчета
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Номер отчета
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Идентификатор возвращаемого товара
        /// </summary>
        public long StockProductId { get; set; }

        /// <summary>
        /// Доступное для возврата кол-во
        /// </summary>
        public decimal AvailableCountToRefund { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Доступная для возврата ставка НДС
        /// </summary>
        public NdsType NdsType { get; set; }

        /// <summary>
        /// Самая поздняя дата отгрузки
        /// </summary>
        public DateTime ShipmentDate { get; set; }
    }
}
