using System.Collections.Generic;
using Moedelo.BizV2.Dto.Calendar;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BizV2.Client.Calendar
{
    [InjectAsSingleton]
    public class CalendarEventProcessingApiClient : BaseApiClient, ICalendarEventProcessingApiClient
    {
        private readonly SettingValue apiEndPoint;

        public CalendarEventProcessingApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("BizPrivateApiEndpoint");
        }

        public Task<CalendarEventStatusChangeResponseDto> StartCalendarEventCloseAsync(int firmId, int userId, CalendarEventStatusChangeRequestDto request)
        {
            return PostAsync<CalendarEventStatusChangeRequestDto,CalendarEventStatusChangeResponseDto>($"/StartEventClose?firmId={firmId}&userId={userId}", request);
        }

        public Task<CalendarEventStatusChangeResponseDto> StartCalendarEventReopenAsync(int firmId, int userId, CalendarEventStatusChangeRequestDto request)
        {
            return PostAsync<CalendarEventStatusChangeRequestDto, CalendarEventStatusChangeResponseDto>($"/StartEventReopen?firmId={firmId}&userId={userId}", request);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/BizFirmCalendarEventProcessing";
        }
    }
}
