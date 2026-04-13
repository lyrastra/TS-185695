using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.ClosingPeriod;

namespace Moedelo.AccountingV2.Dto.ClosedPeriods
{
    public class CheckClosingMonthDto
    {
        /// <summary>
        /// Причины блокировки закрытия месяца
        /// </summary>
        public List<ClosingPeriodBlockReason> BlockingReasons { get; set; }
    }
}
