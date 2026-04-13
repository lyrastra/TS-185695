using System.Collections.Generic;

namespace Moedelo.AccountingV2.Client.RetailRevenue.Dto
{
    /// <summary>
    /// Розничкая выручка или БСО (Z-отчет)
    /// </summary>
    public class RetailRevenueCollectionDto
    {
        /// <summary>
        /// Общее количество операций
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Список операций
        /// </summary>
        public List<RetailRevenueDto> ResourceList { get; set; }
    }
}