using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Reports;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Dto;
using Moedelo.RptV2.Dto.OutsourceCalendar;

namespace Moedelo.RptV2.Client.OutsourceCalendar
{
    [InjectAsSingleton]
    public class OutsourceCalendarApiClient : BaseApiClient, IOutsourceCalendarApiClient
    {
        private readonly SettingValue apiEndpoint;

        public OutsourceCalendarApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(
            httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("OutsourceCalendarApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task RebuildAsync(IReadOnlyList<int> firmIds)
        {
            return PostAsync("/private/api/v1.0/OutsourceCalendar/Rebuild", firmIds);
        }

        public Task RebuildAllAsync()
        {
            return PostAsync("/private/api/v1.0/OutsourceCalendar/RebuildAll");
        }

        public async Task<List<EventResponseDto>> GetAllEventsAsync()
        {
            var response = await GetAsync<CompatibleCoreListWrapper<EventResponseDto>>(
                    "/private/api/v1.0/Events/GetAllEvents").ConfigureAwait(false);

            return response.data;
        }

        public async Task<List<OutsourceEventTypeDto>> GetEventTypeAutoCompleteAsync(string query)
        {
            var response = await GetAsync<CompatibleCoreListWrapper<OutsourceEventTypeDto>>(
                    "/private/api/v1.0/Events/GetEventTypeAutoComplete",
                    new { query }).ConfigureAwait(false);

            return response.data;
        }

        public async Task<List<OutsourceEventTypeDto>> GetAllEventTypesAsync()
        {
            var response =
                await GetAsync<CompatibleCoreListWrapper<OutsourceEventTypeDto>>("/private/api/v1.0/Events/GetEventTypeAutoComplete")
                    .ConfigureAwait(false);

            return response.data;
        }
        
        public async Task<OutsourceEventTypeDto> CreateEventTypeAsync(OutsourceEventTypeDto eventType)
        {
            var response =
                await PostAsync<OutsourceEventTypeDto, CompatibleCoreDataWrapper<OutsourceEventTypeDto>>(
                    "/private/api/v1.0/Events/CreateEventType",
                    eventType
                ).ConfigureAwait(false);

            return response.data;
        }

        public async Task<OutsourceEventTypeDto> GetEventTypeAsync(OutsourceEventTypeCriteriaDto eventTypeCriteria)
        {
            var response =
                await GetAsync<CompatibleCoreDataWrapper<OutsourceEventTypeDto>>(
                    "/private/api/v1.0/Events/GetEventType",
                    eventTypeCriteria
                ).ConfigureAwait(false);

            return response.data;
        }

        public async Task<EventCreateStatus> CreateEventAsync(EventResponseDto outsourceEvent, int userId)
        {
            var response =
                await PostAsync<EventResponseDto, CompatibleCoreDataWrapper<EventCreateStatus>>(
                        $"/private/api/v1.0/Events/CreateEvent?userId={userId}", outsourceEvent)
                    .ConfigureAwait(false);

            return response.data;
        }

        public Task<AttachEventResultDto> AttachEventToFirmsAsync(AttachEventToFirmsDto eventDto, int userId)
        {
            return PostAsync<AttachEventToFirmsDto, AttachEventResultDto>(
                $"/private/api/v1.0/OutsourceCalendar/AttachEvent?userId={userId}", eventDto);
        }

        public Task DetachEventFromFirmsAsync(DetachEventFromFirmsDto eventDto, int userId)
        {
            return PostAsync($"/private/api/v1.0/OutsourceCalendar/DetachEventPost?userId={userId}", eventDto);
        }

        public async Task<List<FirmCalendarResponseDto>> GetFirmCalendarAsync(FirmCalendarCriteriaDto criteriaDto, CancellationToken cancellationToken)
        {
            const string uri = "/private/api/v1.0/OutsourceCalendar/GetFirmCalendar";

            var response =
                await GetAsync<CompatibleCoreListWrapper<FirmCalendarResponseDto>>(uri,
                        criteriaDto, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return response.data;
        }

        public Task UpdateCalendarEventStatusAsync(int firmId, int eventId, bool status)
        {
            return PostAsync("/private/api/v1.0/OutsourceCalendar/UpdateIsCompletedStatus",
                new
                {
                    FirmId = firmId,
                    CompleteListId = eventId,
                    Status = status
                });
        }

        public Task AssignSendToCalendarEventAsync(int firmId, int calendarId, int documentVersionId)
        {
            return 
                PutAsync<object>($"/private/api/v1.0/OutsourceCalendar/AssignSendToCalendarEvent/{firmId}/{calendarId}/{documentVersionId}", null);
        }

        public Task DetachSendForCalendarEventAsync(int firmId, int calendarId)
        {
            return 
                PutAsync<object>($"/private/api/v1.0/OutsourceCalendar/DetachSendForCalendarEvent/{firmId}/{calendarId}", null);
        }

        public async Task<int> GetCalendarEventForSendAsync(int firmId, int documentVersionId)
        {
            var response =
                await GetAsync<int>(
                    $"/private/api/v1.0/OutsourceCalendar/GetCalendarEventForSend/{firmId}/{documentVersionId}").ConfigureAwait(false);

            return response;
        }
    }
}