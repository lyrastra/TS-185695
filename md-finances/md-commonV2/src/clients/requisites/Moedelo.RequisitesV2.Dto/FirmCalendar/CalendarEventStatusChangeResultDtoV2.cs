using Moedelo.Common.Enums.Enums.Calendar;

namespace Moedelo.RequisitesV2.Dto.FirmCalendar
{
    public class CalendarEventStatusChangeResultDtoV2
    {
        public EventChangeStatusResult Status { get; set; } = EventChangeStatusResult.Changed;
        public string UserMessage { get; set; } = string.Empty;
    }
}