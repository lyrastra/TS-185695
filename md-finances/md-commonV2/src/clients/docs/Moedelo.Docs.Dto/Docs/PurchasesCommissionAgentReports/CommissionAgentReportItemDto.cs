using System;

namespace Moedelo.Docs.Dto.Docs.PurchasesCommissionAgentReports
{
    /// <summary>
    /// Позиция отчета комиссионера
    /// </summary>
    public class CommissionAgentReportItemDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public long StockProductId { get; set; }
        
        /// <summary>
        /// Кол-во
        /// </summary>
        public decimal Count { get; set; }
        
        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }
        
        /// <summary>
        /// При возврате: идентификатор отчета комиссионера, в котором была продажа 
        /// </summary>
        public long? RefundCommissionAgentReportId { get; set; }

        /// <summary>
        /// Дата отгрузки/дата возврата
        /// </summary>
        public DateTime? ShipmentDate { get; set; }
    }
}