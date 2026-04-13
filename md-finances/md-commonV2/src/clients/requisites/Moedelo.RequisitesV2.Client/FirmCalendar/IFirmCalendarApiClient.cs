using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Calendar;
using Moedelo.RequisitesV2.Dto.FirmCalendar;

namespace Moedelo.RequisitesV2.Client.FirmCalendar
{
    public interface IFirmCalendarApiClient
    {
        Task AddEvents(int firmId, int userId, IEnumerable<CalendarUserEventDto> @events);
        Task RemoveEvents(int firmId, int userId, RemoveUserEventsRequest request);
        Task<List<CalendarEventDto>> GetEventsAsync(int firmId, int userId, EventsByTypesRequest request);
        Task<List<CalendarEventDto>> GetFirmCalendarForWidgetAsync(int firmId, int userId);
        Task<List<CalendarEventDto>> GetEventsFromReadOnlyAsync(int firmId, int userId, EventsByTypesRequest request);
        Task<List<CalendarEventDto>> GetAllEventsFromReadOnlyAsync(int firmId, int userId, bool excludeNextYearsEvents = true);

        Task SetEventsStatusAsync(int firmId, int userId, List<CalendarEventUidDto> events, CalendarEventStatus status);
        Task<List<CalendarUserEventDto>> GetUserEvents(GetUserEventsRequestDto request);
        Task<List<CalendarEventDto>> GetEventsListAsync(int firmId, int userId, CalendarEventProtoId protoId, bool excludeNextYearsEvents = true, CancellationToken cancellationToken = default(CancellationToken));
        Task<CalendarEventDto> GetEventAsync(int firmId, int userId, int eventId, CalendarEventType eventType);

        Task<List<CompletedReportsStatisticDto>> GetNumberOfCompletedMasterEventsAsync(IList<int> firmIdsList, int year);

        Task<List<CompletedReportsStatisticDto>> GetNumberOfCompletedMasterEventsForAllYearsByProtoIdsAsync(
            IList<int> firmIds, IList<CalendarEventProtoId> protoIds);

        Task Rebuild(int firmId, int userId);

        Task<List<CalendarEventDto>> GetEventsListHotDataAsync(int firmId, int userId, CalendarEventProtoId protoId, bool excludeNextYearsEvents = true);

        Task<CalendarEventDto> GetPrevCloseAccountingPeriodEventAsync(int firmId, int userId);

        Task CreateBizCalendarAsync(int firmId, int userId);

        Task<List<CalendarEventDto>> AllEventsAsync(int firmId, int userId);

        Task<List<CommonCalendarEventDto>> GetCalendarAsync(bool excludeCustomCommonEvents = false);

        Task<CommonCalendarEventDto> GetCalendarAsync(int eventId);

        Task UpdateCalendarAsync(int firmId, int userId, CommonCalendarEventDto eventDto);
        Task<List<CalendarEventDto>> GetFirmCalendarAsync(int firmId, int userId);

        Task<CalendarEventStatusChangeResultDtoV2> CompleteEventAsync(int firmId, int userId, int eventId, CalendarEventType eventType);

        Task<CalendarEventStatusChangeResultDtoV2> ReopenEventAsync(int firmId, int userId, int eventId,
            CalendarEventType eventType);
    }
}
