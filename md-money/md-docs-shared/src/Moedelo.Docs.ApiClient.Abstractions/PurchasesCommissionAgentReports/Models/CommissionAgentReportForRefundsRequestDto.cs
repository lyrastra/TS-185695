using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models
{
    /// <summary>
    /// Запрос отчёта посредника (коммисионера) которого можно указать в возвратах
    /// </summary>
    public class CommissionAgentReportForRefundsRequestDto
    {
        /// <summary>
        /// Идентификатор товара текущего возврата
        /// </summary>
        public long StockProductId { get; set; }

        /// <summary>
        /// Идентификаторы подходящих комиссионеров
        /// </summary>
        public int[] CommissionAgentIds { get; set; }

        /// <summary>
        /// Идентификаторы подходящих договоров
        /// </summary>
        public long[] ContractIds { get; set; }

        /// <summary>
        /// Дата текущего возврата
        /// </summary>
        [Range(typeof(DateTime), "01/01/2000", "01/01/2999")]
        public DateTime ShipmentDate { get; set; }
    }
}
