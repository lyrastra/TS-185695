using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Calendar;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.CalendarEvent;
using Moedelo.RequisitesV2.Dto.FirmCalendar;

namespace Moedelo.RequisitesV2.Client.CalendarEvent
{
    public interface ICalendarEventApiClient : IDI
    {
        Task<List<GetCalendarEventsResponseDto>> GetCalendarEvents(GetCalendarEventsRequestDto request);
    }
}