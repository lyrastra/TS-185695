using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.System.Extensions.Collections;
using Moedelo.Requisites.ApiClient.Abstractions;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(ICalendarEventApiClient))]
    public class CalendarEventApiClient : BaseLegacyApiClient, ICalendarEventApiClient
    {
        public CalendarEventApiClient(
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

        public async Task<GetCalendarEventsResponseDto[]> GetCalendarEventsAsync(GetCalendarEventsRequestDto request)
        {
            var uri = $"/CalendarEvent/GetCalendarEvents";
            var response = await PostAsync<GetCalendarEventsRequestDto, GetCalendarEventsResponseDto[]>(uri, request).ConfigureAwait(false);
            return response;
        }
        
        public Task<CalendarUserEventDto[]> GetUserEventsByIdsAsync(FirmId firmId, UserId userId, 
            IReadOnlyCollection<int> eventIds)
        {
            if (eventIds?.Any() != true)
            {
                return Task.FromResult(Array.Empty<CalendarUserEventDto>());
            }

            var uri = $"/CalendarEvent/UserEventsByIds?firmId={firmId}&userId={userId}";
            
            return PostAsync<IReadOnlyCollection<int>, CalendarUserEventDto[]>(uri,
                eventIds.ToDistinctReadOnlyCollection());
        }
    }
}