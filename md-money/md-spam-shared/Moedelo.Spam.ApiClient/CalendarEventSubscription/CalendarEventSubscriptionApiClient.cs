using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Spam.ApiClient.Abastractions.Dto.CalendarEventSubscription;
using Moedelo.Spam.ApiClient.Abastractions.Dto.Common;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.CalendarEventSubscription;

namespace Moedelo.Spam.ApiClient.CalendarEventSubscription
{
    [InjectAsSingleton(typeof(ICalendarEventSubscriptionApiClient))]
    internal sealed class CalendarEventSubscriptionApiClient : BaseApiClient, ICalendarEventSubscriptionApiClient
    {
        private const string ApiRoute = "/api/v1/CalendarEventSubscription";

        public CalendarEventSubscriptionApiClient(
            IHttpRequestExecuter httpRequestExecutor,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<CalendarEventSubscriptionApiClient> logger)
            : base(
                httpRequestExecutor,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("SpamApiEndpoint"),
                logger)
        { }

        public Task<ApiPageResponseDto<CalendarEventSubscriptionResponseDto>> GetPagedListAsync(
            CalendarEventSubscriptionRequestDto dto,
            CancellationToken cancellationToken)
        {
            return PostAsync<CalendarEventSubscriptionRequestDto,
                ApiPageResponseDto<CalendarEventSubscriptionResponseDto>>($"/{ApiRoute}/GetPagedList", dto,
                cancellationToken: cancellationToken);
        }

        public Task UpdateLastEmailAndSmsProcessingDateByFirmIdsAsync(
            UpdateLastEmailAndSmsProcessingDateRequestDto dto,
            CancellationToken cancellationToken)
        {
            return PostAsync($"/{ApiRoute}/UpdateLastEmailAndSmsProcessingDateByFirmIds", dto,
                cancellationToken: cancellationToken);
        }
    }
}