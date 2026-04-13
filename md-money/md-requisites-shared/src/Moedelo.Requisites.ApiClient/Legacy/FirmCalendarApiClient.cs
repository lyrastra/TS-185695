using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.ApiClient.Legacy.Wrappers;
using Moedelo.Requisites.Enums.Calendar;

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IFirmCalendarApiClient))]
    internal sealed class FirmCalendarApiClient : BaseLegacyApiClient, IFirmCalendarApiClient
    {
        public FirmCalendarApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FirmCalendarApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public async Task<CalendarEventDto[]> GetEventsAsync(FirmId firmId, UserId userId, EventsByTypesRequestDto request, bool fromReadOnly = false)
        {
            var uri = fromReadOnly
                ? $"/FirmCalendar/EventsByProtoIdsFromReadOnly?firmId={firmId}&userId={userId}"
                : $"/FirmCalendar/EventsByProtoIds?firmId={firmId}&userId={userId}";
            var response = await PostAsync<EventsByTypesRequestDto, ListResponseWrapper<CalendarEventDto>>(uri, request).ConfigureAwait(false);
            return response.Items;
        }

        public async Task<CalendarEventDto> GetEventAsync(FirmId firmId, UserId userId, int eventId, CalendarEventType eventType)
        {
            var uri = $"/FirmCalendar/GetEvent?firmId={firmId}&userId={userId}&eventId={eventId}&eventType={eventType}";


            /* Unmerged change from project 'Moedelo.Requisites.ApiClient (net6.0)'
            Before:
                        var response = await GetAsync<Wrappers.DataResponseWrapper<CalendarEventDto>>(uri).ConfigureAwait(false);
            After:
                        var response = await GetAsync<DataResponseWrapper<CalendarEventDto>>(uri).ConfigureAwait(false);
            */
            var response = await GetAsync<DataResponseWrapper<CalendarEventDto>>(uri).ConfigureAwait(false);
            return response?.Data ?? null;
        }


        public async Task<bool> SetEventsStatusAsync(FirmId firmId, UserId userId,
            CalendarEventStatusChangeRequestDto request)
        {
            var uri = $"/FirmCalendar/SetEventsStatus?firmId={firmId}&userId={userId}";
            var response = await PostAsync<CalendarEventStatusChangeRequestDto, StatusResponseWrapper>(uri, request);

            return response?.ResponseStatus ?? false;

        }

        public async Task<CalendarEventDto[]> GetAllEventsAsync(FirmId firmId, UserId userId, bool excludeNextYearsEvents = true, bool fromReadOnly = false)
        {
            var uri = fromReadOnly
                ? "/FirmCalendar/AllEventsFromReadOnly"
                : "/FirmCalendar/AllEvents";
            var param = new { firmId, userId, excludeNextYearsEvents };
            var response = await GetAsync<ListResponseWrapper<CalendarEventDto>>(uri, param).ConfigureAwait(false);
            return response.Items;
        }

        public async Task<CalendarEventDto[]> GetEventsByProtoIdsAsync(FirmId firmId, UserId userId, EventsListByProtoIdsRequestDto request)
        {
            var response = await PostAsync<EventsListByProtoIdsRequestDto, ListResponseWrapper<CalendarEventDto>>(
                $"/FirmCalendar/EventsByProtoIds?firmId={firmId}&userId={userId}", request).ConfigureAwait(false);
            return response.Items;
        }

        public Task RebuildAsync(FirmId firmId, UserId userId)
        {
            return GetAsync<Task>("/FirmCalendar/Rebuild", new { firmId, userId }, setting: new HttpQuerySetting(TimeSpan.FromSeconds(60)));
        }

        public Task RebuildEdsEventsAsync()
        {
            return GetAsync<Task>("/FirmCalendar/RebuildEdsEvents", setting: new HttpQuerySetting(TimeSpan.FromSeconds(60)));
        }

        public async Task<DateTime?> GetLastUsnCalendarEventDateAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/FirmCalendar/LastUsnCalendarEventDate?firmId={firmId}&userId={userId}";

            var response = await GetAsync<DataResponseWrapper<DateTime?>>(uri).ConfigureAwait(false);

            return response?.Data;
        }
    }
}
