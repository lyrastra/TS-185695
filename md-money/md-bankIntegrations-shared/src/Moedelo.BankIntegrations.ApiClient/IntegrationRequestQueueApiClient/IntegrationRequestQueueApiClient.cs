using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.IntegrationRequestQueueApiClient;
using Moedelo.BankIntegrations.ApiClient.Dto.RepeatHandler;
using Moedelo.BankIntegrations.Enums;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.IntegrationRequestQueueApiClient
{
    [InjectAsSingleton(typeof(IIntegrationRequestQueueApiClient))]
    public class IntegrationRequestQueueApiClient : BaseApiClient, IIntegrationRequestQueueApiClient
    {
        private const string ControllerPath = "/private/api/v1/IntegrationRequestQueue";

        public IntegrationRequestQueueApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<IntegrationRequestQueueApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("IntegrationApiNetCore"),
                logger)
        {
        }

        public Task CreateEventAsync(RepeatEventCreateRequestDto request, CancellationToken cancellationToken = default)
        {
            var url = $"{ControllerPath}/CreateEvent";
            return PostAsync(url, request, cancellationToken: cancellationToken);
        }

        public async Task<IReadOnlyList<RepeatEventDto>> GetReadyEventsAsync(REventType type, CancellationToken cancellationToken = default)
        {
            var url = $"{ControllerPath}/GetReadyEvents";
            var events = await GetAsync<RepeatEventDto[]>(url, new { type = (int)type }, cancellationToken: cancellationToken);
            return events;
        }

        public async Task<IReadOnlyList<RepeatEventDto>> TakeEventsInWorkAsync(REventType type, CancellationToken cancellationToken = default)
        {
            var url = $"{ControllerPath}/{(int)type}/TakeEventsInWork";
            var request = new RepeatEventTakeRequestDto { Type = type };
            var events = await PostAsync<RepeatEventTakeRequestDto, RepeatEventDto[]>(url, request, cancellationToken: cancellationToken);
            return events;
        }

        public Task TakeEventReadyAsync(int eventId, RepeatEventReadyRequestDto request, CancellationToken cancellationToken = default)
        {
            var url = $"{ControllerPath}/{eventId}/TakeEventReady";
            return PutAsync(url, request, cancellationToken: cancellationToken);
        }

        public Task UpdateEventRetryDateAsync(int eventId, RepeatEventRetryDateRequestDto request, CancellationToken cancellationToken = default)
        {
            var url = $"{ControllerPath}/{eventId}/UpdateRetryDate";
            return PutAsync(url, request, cancellationToken: cancellationToken);
        }

        public Task DeleteEventAsync(int eventId, CancellationToken cancellationToken = default)
        {
            var url = $"{ControllerPath}/{eventId}";
            return DeleteAsync(url, cancellationToken: cancellationToken);
        }

        public Task DeleteEventsAsync(RepeatEventDeleteRequestDto request, CancellationToken cancellationToken = default)
        {
            var url = $"{ControllerPath}/DeleteMany";
            return DeleteByRequestAsync(url, request, cancellationToken: cancellationToken);
        }
    }
}
