using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Calendar;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.FirmCalendar;

namespace Moedelo.RequisitesV2.Client.FirmCalendar
{
    [InjectAsSingleton(typeof(IFirmCalendarApiClient))]
    public class FirmCalendarApiClient : BaseApiClient, IFirmCalendarApiClient
    {
        private readonly SettingValue apiEndPoint;

        public FirmCalendarApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task AddEvents(int firmId, int userId, IEnumerable<CalendarUserEventDto> @events) =>
            PostAsync(
                uri: $"/FirmCalendar/AddUserEvents?firmId={firmId}&userId={userId}",
                data: events);

        public Task RemoveEvents(int firmId, int userId, RemoveUserEventsRequest request) =>
            PostAsync(
                uri: $"/FirmCalendar/DeleteUserEvents?firmId={firmId}&userId={userId}",
                data: request);

        public async Task<List<CalendarEventDto>> GetEventsAsync(int firmId, int userId, EventsByTypesRequest request)
        {
            var uri = $"/FirmCalendar/EventsByProtoIds?firmId={firmId}&userId={userId}";
            var response = await PostAsync<EventsByTypesRequest, GetEventsListResponseDto>(uri, request).ConfigureAwait(false);

            return response.Items;
        }

        public async Task<List<CalendarEventDto>> GetEventsFromReadOnlyAsync(int firmId, int userId, EventsByTypesRequest request)
        {
            var uri = $"/FirmCalendar/EventsByProtoIdsFromReadOnly?firmId={firmId}&userId={userId}";
            var response = await PostAsync<EventsByTypesRequest, GetEventsListResponseDto>(uri, request).ConfigureAwait(false);

            return response.Items;
        }

        public async Task<List<CalendarEventDto>> GetAllEventsFromReadOnlyAsync(int firmId, int userId, bool excludeNextYearsEvents =  true)
        {
            var response = await GetAsync<GetEventsListResponseDto>($"/FirmCalendar/AllEventsFromReadOnly",
                new { firmId, userId, excludeNextYearsEvents}).ConfigureAwait(false);

            return response.Items;
        }

        public Task SetEventsStatusAsync(int firmId, int userId, List<CalendarEventUidDto> events, CalendarEventStatus status)
        {
            var dto = new CalendarEventStatusChangeRequestDto
            {
                Events = events,
                Status = status
            };

            return PostAsync($"/FirmCalendar/SetEventsStatus?firmId={firmId}&userId={userId}", dto);
        }

        public Task<List<CalendarUserEventDto>> GetUserEvents(GetUserEventsRequestDto request)
        {
            return PostAsync<GetUserEventsRequestDto, List<CalendarUserEventDto>>(
                $"/FirmCalendar/GetUserEvents", request);
        }

        public async Task<List<CalendarEventDto>> GetEventsListAsync(int firmId, int userId,
            CalendarEventProtoId protoId, bool excludeNextYearsEvents = true,
            CancellationToken cancellationToken = default)
        {
            var response = await GetAsync<GetEventsListResponseDto>("/FirmCalendar/EventsByProtoId",
                new {firmId, userId, protoId, excludeNextYearsEvents}, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return response.Items;
        }

        public async Task<CalendarEventDto> GetEventAsync(int firmId, int userId, int eventId, CalendarEventType eventType)
        {
            var response = await GetAsync<GetEventResponseDto>("/FirmCalendar/GetEvent", new { firmId, userId, eventId, eventType }).ConfigureAwait(false);
            return response.Data;
        }

        public Task<List<CompletedReportsStatisticDto>> GetNumberOfCompletedMasterEventsAsync(IList<int> firmIdsList, int year)
        {
            var p = new CountOfCompletedMastersEventsRequest
            {
                FirmIdsList = firmIdsList,
                Year = year
            };
            return PostAsync<CountOfCompletedMastersEventsRequest, List<CompletedReportsStatisticDto>>("/FirmCalendar/NumberOfCompletedMasterEvents", p);
        }

        public Task<List<CompletedReportsStatisticDto>> GetNumberOfCompletedMasterEventsForAllYearsByProtoIdsAsync(
            IList<int> firmIds, IList<CalendarEventProtoId> protoIds)
        {
            var dto = new CountOfCompletedMastersEventsByProtoIdsRequest
            {
                FirmIds = firmIds,
                ProtoIds = protoIds
            };

            return PostAsync<CountOfCompletedMastersEventsByProtoIdsRequest, List<CompletedReportsStatisticDto>>(
                "/FirmCalendar/GetNumberOfCompletedMasterEventsForAllYearsByProtoIds", dto);
        }

        public Task Rebuild(int firmId, int userId)
        {
            return GetAsync("/FirmCalendar/Rebuild", new { firmId, userId });
        }

        public async Task<List<CalendarEventDto>> GetEventsListHotDataAsync(int firmId, int userId, CalendarEventProtoId protoId,
            bool excludeNextYearsEvents = true)
        {
            var response = await GetAsync<GetEventsListResponseDto>(
                "/FirmCalendar/EventsByProtoIdHotData",
                new
                {
                    firmId,
                    userId,
                    protoId,
                    excludeNextYearsEvents
                }).ConfigureAwait(false);

            return response.Items;
        }

        public async Task<CalendarEventDto> GetPrevCloseAccountingPeriodEventAsync(int firmId, int userId)
        {
            var result = await GetAsync<DataWrapper<CalendarEventDto>>(
                "/FirmCalendar/GetPrevCloseAccountingPeriodEvent",
                new
                {
                    firmId,
                    userId
                }).ConfigureAwait(false);

            return result.Data;
        }

        public Task CreateBizCalendarAsync(int firmId, int userId)
        {
            return PostAsync($"/FirmCalendar/CreateBizCalendar?firmId={firmId}&userId={userId}");
        }

        public async Task<List<CalendarEventDto>> AllEventsAsync(int firmId, int userId)
        {
            var data = await GetAsync<ListWrapper<CalendarEventDto>>("/FirmCalendar/AllEvents", new { firmId, userId }).ConfigureAwait(false);
            return data.Items;
        }

        public async Task<List<CalendarEventDto>> GetFirmCalendarAsync(int firmId, int userId)
        {
            var data = await GetAsync<ListWrapper<CalendarEventDto>>("/FirmCalendar/FirmCalendarEvents", new { firmId, userId }).ConfigureAwait(false);
            return data.Items;
        }

        public async Task<List<CalendarEventDto>> GetFirmCalendarForWidgetAsync(int firmId, int userId)
        {
            var data = await GetAsync<ListWrapper<CalendarEventDto>>("/FirmCalendar/FirmCalendarEventsForWidget", new { firmId, userId }).ConfigureAwait(false);
            return data.Items;
        }

        public async Task<List<CommonCalendarEventDto>> GetCalendarAsync(bool excludeCustomCommonEvents = false)
        {
            var data = await GetAsync<ListWrapper<CommonCalendarEventDto>>("/Calendar/GetCalendar", new { excludeCustomCommonEvents }).ConfigureAwait(false);
            return data.Items;
        }

        public Task<CommonCalendarEventDto> GetCalendarAsync(int eventId)
        {
            return GetAsync<CommonCalendarEventDto>("/Calendar/GetCalendar", new { eventId });
        }

        public Task UpdateCalendarAsync(int firmId, int userId, CommonCalendarEventDto eventDto)
        {
            return PostAsync($"/Calendar/UpdateCalendar?firmId={firmId}&userId={userId}", eventDto);
        }

        public Task<CalendarEventStatusChangeResultDtoV2> CompleteEventAsync(int firmId, int userId, int eventId, CalendarEventType eventType)
        {
            var requestDto = new CalendarEventStatusChangeRequestDtoV2()
            {
                EventId = eventId,
                EventType = eventType
            };

            return PostAsync<CalendarEventStatusChangeRequestDtoV2, CalendarEventStatusChangeResultDtoV2>
                ($"/FirmCalendar/CompleteEventFromCalendar?firmId={firmId}&userId={userId}", requestDto);
        }

        public Task<CalendarEventStatusChangeResultDtoV2> ReopenEventAsync(int firmId, int userId, int eventId,
            CalendarEventType eventType)
        {
            return PostAsync<object, CalendarEventStatusChangeResultDtoV2>
                ($"/FirmCalendar/ReopenEvent?firmId={firmId}&userId={userId}", new
            {
                Id = eventId,
                Type = eventType
            });
        }
    }
}
