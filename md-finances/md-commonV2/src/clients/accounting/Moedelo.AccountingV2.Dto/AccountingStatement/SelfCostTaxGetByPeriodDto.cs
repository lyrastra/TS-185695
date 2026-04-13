using System;

namespace Moedelo.AccountingV2.Dto.AccountingStatement
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
