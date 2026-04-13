using System;

namespace Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos.SelfCostTax
{
    public class SelfCostTaxDeleteByPeriodDto
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