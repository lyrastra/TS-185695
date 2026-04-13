using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Spam.ApiClient.Abastractions.Dto.CalendarEventSubscription;
using Moedelo.Spam.ApiClient.Abastractions.Dto.Common;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.CalendarEventSubscription;

namespace Moedelo.Spam.ApiClient.Framework.CalendarEventSubscription
{
    [InjectAsSingleton(typeof(ICalendarEventSubscriptionApiClient))]
    internal sealed class CalendarEventSubscriptionApiClient : BaseApiClient, ICalendarEventSubscriptionApiClient
    {
        private const string ApiRoute = "/api/v1/CalendarEventSubscription";

        private readonly SettingValue apiEndPoint;

        public CalendarEventSubscriptionApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("SpamApiEndpoint");
        }

        public Task<ApiPageResponseDto<CalendarEventSubscriptionResponseDto>> GetPagedListAsync(
            CalendarEventSubscriptionRequestDto dto,
            CancellationToken cancellationToken)
        {
            return PostAsync<CalendarEventSubscriptionRequestDto,
               ApiPageResponseDto<CalendarEventSubscriptionResponseDto>>($"{ApiRoute}/GetPagedList", dto, 
               cancellationToken: cancellationToken);
        }

        public Task UpdateLastEmailAndSmsProcessingDateByFirmIdsAsync(
            UpdateLastEmailAndSmsProcessingDateRequestDto dto, 
            CancellationToken cancellationToken)
        {
            return PostAsync($"{ApiRoute}/UpdateLastEmailAndSmsProcessingDateByFirmIds", dto,
                cancellationToken: cancellationToken);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}