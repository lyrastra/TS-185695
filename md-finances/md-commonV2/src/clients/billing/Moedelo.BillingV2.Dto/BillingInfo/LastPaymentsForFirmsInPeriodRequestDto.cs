using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Moedelo.BillingV2.Dto.BillingInfo
{
    public enum PeriodType
    {
        [Description("Неизвестно")]
        None = 0,
        
        [Description("Период продажи")]
        ByDate = 1,

        [Description("Период окончания доступа")]
        ByEndDate = 2
    }

    public class LastPaymentsForFirmsInPeriodRequestDto
    {
        public PeriodType PeriodType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IReadOnlyCollection<int> FirmIds { get; set; }

        public IReadOnlyCollection<int> PriceListIds { get; set; }
    }
}