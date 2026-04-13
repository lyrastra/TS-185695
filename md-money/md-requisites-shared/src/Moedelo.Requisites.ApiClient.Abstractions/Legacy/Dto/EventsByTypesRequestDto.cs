using System.Collections.Generic;
using Moedelo.Requisites.Enums.Calendar;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class EventsByTypesRequestDto
    {
        public IReadOnlyCollection<CalendarEventProtoId> ProtoIds { get; set; }
        public bool ExcludeNextYearsEvents { get; set; }
    }
}
