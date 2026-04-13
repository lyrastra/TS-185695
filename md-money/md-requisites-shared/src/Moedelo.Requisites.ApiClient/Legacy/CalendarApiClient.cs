using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Requisites.ApiClient.Abstractions;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.Enums.Calendar;

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(ICalendarApiClient))]
    public class CalendarApiClient : BaseLegacyApiClient, ICalendarApiClient
    {
        public CalendarApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<CalendarEventApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public async Task<IList<CommonCalendarEventDto>> GetCalendarAsync()
        {
            var result = await GetAsync<ListDto<CommonCalendarEventDto>>($"/Calendar/GetCalendar");
            return result.Items;
        }

        public Task<CommonCalendarEventDto> GetAsync(int eventId)
        {
            return GetAsync<CommonCalendarEventDto>($"/Calendar/GetCalendar?eventId={eventId}");
        }
        
        public async Task<IList<CommonCalendarEventDto>> GetCalendarWithCriterionAsync(CommonCalendarCriterionDto criterionDto)
        {
            return await PostAsync<CommonCalendarCriterionDto, List<CommonCalendarEventDto>>($"/Calendar/GetCalendarWithCriterion", criterionDto);
        }
    }
}
