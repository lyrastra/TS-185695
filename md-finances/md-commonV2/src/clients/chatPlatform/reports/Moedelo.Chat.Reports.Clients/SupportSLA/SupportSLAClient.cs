using Moedelo.Chat.Client.Base;
using Moedelo.Chat.Reports.Dto;
using Moedelo.Chat.Reports.Dto.SLA;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.Chat.Reports.Clients.SupportSLA
{
    [InjectAsSingleton]
    public class SupportSLAClient: ChatPlatformBasePrivateApiClient, ISupportSLAClient
    {
        public SupportSLAClient
            (
                IHttpRequestExecutor httpRequestExecutor,
                IUriCreator uriCreator,
                IResponseParser responseParser,
                IAuditTracer auditTracer,
                IAuditScopeManager auditScopeManager,
                ISettingRepository settingRepository
            )
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager, settingRepository)
        {

        }

        public Task<SupportSLASummaryDto> GetSupportSLAReportAsync(DateTime start, DateTime end)
        {
            var data = new BaseReportRequestDto()
            {
                Start = start,
                End = end
            };

            return PostAsync<BaseReportRequestDto, SupportSLASummaryDto>("Report/Support/SLA", data);
        }

        public Task<SupportSLASummaryDto> GetTodaySupportSLAReportAsync()
        {
            var today = DateTime.UtcNow;
            var data = new BaseReportRequestDto()
            {
                Start = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0, 0, DateTimeKind.Utc),
                End = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59, 999, DateTimeKind.Utc)
            };

            return PostAsync<BaseReportRequestDto, SupportSLASummaryDto>("Report/Support/SLA", data);
        }
    }
}
