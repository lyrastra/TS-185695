using System;
using Moedelo.Common.Enums.Enums.Calendar;

namespace Moedelo.RequisitesV2.Dto.CalendarEvent
{
    public class GetCalendarEventsResponseDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public int Year  { get; set; }

        public int Period { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public CalendarEventProtoId EventType { get; set; }

        public CalendarEventStatus EventStatus { get; set; }

        public CalendarEventType CalendarEventType { get; set; }
    }
}