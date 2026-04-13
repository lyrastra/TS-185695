using System;

namespace Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos.TaxSelfCost
{
    public class SelfCostTaxGetByPeriodDto
    {
        /// <summary>
        /// Начало периода (включительно)
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Конец периода (включительно)
        /// </summary>
        public DateTime End { get; set; }
    }
}
