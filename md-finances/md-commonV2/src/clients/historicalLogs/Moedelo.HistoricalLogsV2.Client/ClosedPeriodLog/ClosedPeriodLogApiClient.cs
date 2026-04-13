using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.HistoricalLogsV2.Dto.ClosedPeriodLog;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HistoricalLogsV2.Client.ClosedPeriodLog
{
    [InjectAsSingleton]
    public class ClosedPeriodLogApiClient : BaseApiClient, IClosedPeriodLogApiClient
    {
        private readonly SettingValue endpointSetting;

        public ClosedPeriodLogApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            endpointSetting = settingRepository.Get("HistoricalLogsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return endpointSetting.Value;
        }

        public Task LogAsync(ClosedPeriodLogRequestDto dto)
        {
            return PostAsync("/ClosedPeriodLog/Log", dto);
        }

        public Task<IReadOnlyList<ClosedPeriodLogResponseDto>> ByFirmIdAsync(int firmId)
        {
            return GetAsync<IReadOnlyList<ClosedPeriodLogResponseDto>>("/ClosePeriodLog/ByFirmId", new { firmId });
        }
    }
}
