using System;
using System.Collections.Generic;

namespace Moedelo.RequisitesV2.Dto.FirmCalendar
{
    public class FirmsCalendarForNoticeRequestDto
    {
        public IReadOnlyCollection<int> FirmIds { get; set; }

        public bool IsSmsNotice { get; set; }

        public string OnDate { get; set; }

        public DateTime[] EndDates { get; set; } = null;
    }
}