using System;
using Moedelo.Common.Enums.Enums.Calendar;

namespace Moedelo.RequisitesV2.Dto.FirmCalendar
{
    public class CalendarUserEventDto
    {
        public int Id { get; set; }
        public int FirmId { get; set; }

        public CalendarEventStatus Status { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Year { get; set; }
        public int? Period { get; set; }
    }
}
