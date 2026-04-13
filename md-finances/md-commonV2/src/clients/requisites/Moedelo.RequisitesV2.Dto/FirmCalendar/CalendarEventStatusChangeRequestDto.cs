using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Calendar;

namespace Moedelo.RequisitesV2.Dto.FirmCalendar
{
    public class CalendarEventStatusChangeRequestDto
    {
        public List<CalendarEventUidDto> Events { get; set; }
        public CalendarEventStatus Status { get; set; }
    }
}
