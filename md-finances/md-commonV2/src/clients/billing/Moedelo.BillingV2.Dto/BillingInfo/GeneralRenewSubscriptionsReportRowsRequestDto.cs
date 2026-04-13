using System;
using System.Collections.Generic;

namespace Moedelo.BillingV2.Dto.BillingInfo
{
    public class GeneralRenewSubscriptionsReportRowsRequestDto
    {
        /// <summary>
        /// дата начала периода переподписки
        /// </summary>
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// дата завершения периода переподписки
        /// </summary>
        public DateTime DateUntil { get; set; }
        /// <summary>
        /// перечень продуктов, по которым должен быть построен отчёт
        /// </summary>
        public IReadOnlyCollection<string> Products { get; set; }
        /// <summary>
        /// методы оплаты, которые не должны быть включены в отчёт
        /// </summary>
        public IReadOnlyCollection<string> ExcludedMethods { get; set; }
    }
}