using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions
{
    public interface ICalendarEventApiClient
    {
        Task<GetCalendarEventsResponseDto[]> GetCalendarEventsAsync(GetCalendarEventsRequestDto request);
        Task<CalendarUserEventDto[]> GetUserEventsByIdsAsync(FirmId firmId, UserId userId, 
            IReadOnlyCollection<int> eventIds);
    }
}