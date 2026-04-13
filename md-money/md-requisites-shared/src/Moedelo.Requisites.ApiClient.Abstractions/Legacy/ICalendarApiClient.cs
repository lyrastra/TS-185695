using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.Enums.Calendar;

namespace Moedelo.Requisites.ApiClient.Abstractions
{
    public interface ICalendarApiClient
    {
        Task<IList<CommonCalendarEventDto>> GetCalendarAsync();

        Task<CommonCalendarEventDto> GetAsync(int eventId);

        Task<IList<CommonCalendarEventDto>> GetCalendarWithCriterionAsync(CommonCalendarCriterionDto criterionDto);
    }
}