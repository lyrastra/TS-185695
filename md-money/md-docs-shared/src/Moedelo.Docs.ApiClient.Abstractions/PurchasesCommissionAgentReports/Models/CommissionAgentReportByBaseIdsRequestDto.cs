using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models
{
    public class CommissionAgentReportByBaseIdsRequestDto
    {
        /// <summary>
        /// Список идентификаторов
        /// </summary>
        public IReadOnlyCollection<long> BaseIds { get; set; }
        
        /// <summary>
        /// Читать данные с реплики
        /// </summary>
        public bool UseReadOnly { get; set; }
    }
}