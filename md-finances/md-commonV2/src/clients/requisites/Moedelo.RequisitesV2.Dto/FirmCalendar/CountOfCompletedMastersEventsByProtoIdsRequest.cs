using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Calendar;

namespace Moedelo.RequisitesV2.Dto.FirmCalendar
{
    public class CountOfCompletedMastersEventsByProtoIdsRequest
    {
        public IList<int> FirmIds { get; set; }

        public IList<CalendarEventProtoId> ProtoIds { get; set; }
    }
}