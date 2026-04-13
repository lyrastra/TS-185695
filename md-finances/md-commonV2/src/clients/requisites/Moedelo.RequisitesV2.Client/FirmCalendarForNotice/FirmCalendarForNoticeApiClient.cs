using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.FirmCalendar;

namespace Moedelo.RequisitesV2.Client.FirmCalendarForNotice
{
    [InjectAsSingleton]
    public class FirmCalendarForNoticeApiClient : BaseApiClient, IFirmCalendarForNoticeApiClient
    {
        private readonly SettingValue apiEndPoint;

        public FirmCalendarForNoticeApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<List<CalendarEventNoticeDto>> GetCalendarForNoticeAsync(
            FirmCalendarForNoticeRequestDto requestDto)
        {
            var result =
                await GetAsync<ListWrapper<CalendarEventNoticeDto>>("/FirmCalendarForNotice/GetAsync", requestDto).ConfigureAwait(false);

            return result.Items;
        }

        public Task<IReadOnlyDictionary<int, List<CalendarEventNoticeDto>>> GetFirmsCalendarForNoticeAsync(
            FirmsCalendarForNoticeRequestDto requestDto,
            CancellationToken cancellationToken)
        { 
            return PostAsync<FirmsCalendarForNoticeRequestDto,IReadOnlyDictionary<int, List<CalendarEventNoticeDto>>>(
                "/FirmCalendarForNotice/GetAsync",
                requestDto,
                cancellationToken: cancellationToken);
        }
    }
}