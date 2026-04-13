using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Calendar;

namespace Moedelo.RequisitesV2.Dto.FirmCalendar
{
    public class CalendarEventStatusChangeRequestDtoV2
    {
        public int EventId { get; set; }
        public CalendarEventType EventType { get; set; }
    }
}
