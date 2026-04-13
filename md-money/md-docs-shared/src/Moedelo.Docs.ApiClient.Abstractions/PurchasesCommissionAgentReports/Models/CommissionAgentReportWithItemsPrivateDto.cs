using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models
{
    public class CommissionAgentReportWithItemsPrivateDto
    {
        /// <summary>
        /// Тело документа
        /// </summary>
        public CommissionAgentReportPrivateDto Document { get; set; }
        
        /// <summary>
        /// Позиции документа
        /// </summary>
        public IReadOnlyCollection<CommissionAgentReportItemPrivateDto> Items { get; set; }
    }
}