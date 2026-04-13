using System;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto
{
    /// <summary>
    /// Параметры запроса выписок
    /// </summary>
    public class StatementRequestDto
    {
        /// <summary>
        /// Наачало периода
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Конец периода
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// Не запрашивать выписку, если в очереди выписок есть необработанные 
        /// </summary>
        public bool StopOnUnProcessedRequest { get; set; }
    }
}