using System.Collections.Generic;

namespace Moedelo.Docs.Dto.Docs.PurchasesCommissionAgentReports
{
    /// <summary>
    /// Отчета комиссионера: документ и позиции
    /// </summary>
    public class CommissionAgentReportWithItemsDto
    {
        /// <summary>
        /// Тело
        /// </summary>
        public CommissionAgentReportShortDto Document { get; set; }
        
        /// <summary>
        /// Позиции
        /// </summary>
        public List<CommissionAgentReportItemDto> Items { get; set; }
    }
}