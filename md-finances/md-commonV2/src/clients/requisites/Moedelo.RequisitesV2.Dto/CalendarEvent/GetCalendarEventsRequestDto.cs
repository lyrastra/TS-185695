using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Calendar;

namespace Moedelo.RequisitesV2.Dto.CalendarEvent
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