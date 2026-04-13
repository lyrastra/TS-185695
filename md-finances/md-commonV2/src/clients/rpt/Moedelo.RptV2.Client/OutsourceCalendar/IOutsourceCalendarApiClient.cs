using System.Collections.Generic;
using System.Threading;
using Moedelo.Common.Enums.Enums.Reports;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RptV2.Dto.OutsourceCalendar;

namespace Moedelo.RptV2.Client.OutsourceCalendar
{
    public interface IOutsourceCalendarApiClient : IDI
    {
        Task RebuildAsync(IReadOnlyList<int> firmIds);

        Task RebuildAllAsync();

        Task<List<EventResponseDto>> GetAllEventsAsync();

        Task<List<OutsourceEventTypeDto>> GetEventTypeAutoCompleteAsync(string query);

        Task<List<OutsourceEventTypeDto>> GetAllEventTypesAsync();

        Task<OutsourceEventTypeDto> GetEventTypeAsync(OutsourceEventTypeCriteriaDto eventTypeCriteria);

        Task<EventCreateStatus> CreateEventAsync(EventResponseDto outsourceEvent, int userId);

        Task<OutsourceEventTypeDto> CreateEventTypeAsync(OutsourceEventTypeDto eventType);

        Task<List<FirmCalendarResponseDto>> GetFirmCalendarAsync(FirmCalendarCriteriaDto criteriaDto, CancellationToken cancellationToken = default);

        Task UpdateCalendarEventStatusAsync(int firmId, int eventId, bool status);

        Task<AttachEventResultDto> AttachEventToFirmsAsync(AttachEventToFirmsDto eventToAssign, int userId);

        Task DetachEventFromFirmsAsync(DetachEventFromFirmsDto eventToDelete, int userId);

        Task AssignSendToCalendarEventAsync(int firmId, int calendarId, int documentVersionId);

        Task DetachSendForCalendarEventAsync(int firmId, int calendarId);

        Task<int> GetCalendarEventForSendAsync(int firmId, int documentVersionId);
    }
}