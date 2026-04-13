using System;

namespace Moedelo.RptV2.Dto.OutsourceCalendar
{
    public class FirmCalendarCriteriaDto
    {
        public int? FirmId { get; set; }

        public int? CalendarCompleteListId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        
        public int? EventQuarter { get; set; }
        
        public int? EventYear { get; set; }
    }
}