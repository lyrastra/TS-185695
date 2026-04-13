using System;
using Moedelo.Requisites.Enums.Calendar;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
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