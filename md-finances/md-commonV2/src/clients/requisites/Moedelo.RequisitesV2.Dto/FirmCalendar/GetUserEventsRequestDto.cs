using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Calendar;

namespace Moedelo.RequisitesV2.Dto.FirmCalendar
{
    public class GetUserEventsRequestDto
    {
        public List<int> FirmIds { get; set; }
        public List<CalendarEventProtoId> ProtoIds { get; set; }
        public int? Period { get; set; }
        public int? Year { get; set; }
    }
}
