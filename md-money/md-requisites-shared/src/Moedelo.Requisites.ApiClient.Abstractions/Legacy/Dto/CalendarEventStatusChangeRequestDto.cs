using System.Collections.Generic;
using Moedelo.Requisites.Enums.Calendar;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class CalendarEventStatusChangeRequestDto
    {
        public List<CalendarEventUidDto> Events { get; set; }
        public CalendarEventStatus Status { get; set; }
    }
}