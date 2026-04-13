using System;
using Moedelo.Requisites.Enums.Calendar;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class CalendarEventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastDateInPeriod { get; set; }
        public CalendarEventStatus Status { get; set; }
        public CalendarEventProtoId ProtoId { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public int PeriodNumber { get; set; }
        public string PeriodType { get; set; }
        public CalendarEventType EventType { get; set; }
    }
}
