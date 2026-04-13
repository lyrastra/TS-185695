using System;
using System.Collections.Generic;
using Moedelo.Requisites.Enums.Calendar;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class GetCalendarEventsRequestDto
    {
        public List<int> FirmIds { get; set; }
        
        public DateTime OnDate { get; set; }
        
        public CalendarEventProtoId EventType { get; set; }

        public CalendarEventStatus EventStatus { get; set; }
        
        public CalendarEventType CalendarEventType { get; set; }
    }
}