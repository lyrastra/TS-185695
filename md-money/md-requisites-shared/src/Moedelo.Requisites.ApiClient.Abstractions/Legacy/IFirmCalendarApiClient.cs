using System;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.Enums.Calendar;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface IFirmCalendarApiClient
    {
        Task<CalendarEventDto[]> GetEventsAsync(FirmId firmId, UserId userId, EventsByTypesRequestDto request,
            bool fromReadOnly = false);

        Task<CalendarEventDto> GetEventAsync(FirmId firmId, UserId userId, int eventId, CalendarEventType eventType);

        Task<bool> SetEventsStatusAsync(FirmId firmId, UserId userId, CalendarEventStatusChangeRequestDto request);

        Task<CalendarEventDto[]> GetAllEventsAsync(FirmId firmId, UserId userId, bool excludeNextYearsEvents = true, bool fromReadOnly = false);

        Task<CalendarEventDto[]> GetEventsByProtoIdsAsync(FirmId firmId, UserId userId, EventsListByProtoIdsRequestDto request);

        Task RebuildAsync(FirmId firmId, UserId userId);

        /// <summary>
        /// Перерасчитывает события по ЭЦП для всех фирм без перестроения календаря (аналог консоли)
        /// </summary>
        Task RebuildEdsEventsAsync();

        Task<DateTime?> GetLastUsnCalendarEventDateAsync(FirmId firmId, UserId userId);
    }
}
