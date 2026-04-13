using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Dto.WebStatistics;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AgentsV2.Client.WebStatistic
{
    [InjectAsSingleton]
    public class WebStatisticApiClient : BaseApiClient, IWebStatisticApiClient
    {
        private readonly SettingValue apiEndPoint;

        public WebStatisticApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager
            ) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AgentsApiUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<WebStatisticsResponseDto> GetStatisticByPartnerAsync(WebStatisticsRequestDto request)
        {
            return GetAsync<WebStatisticsResponseDto>("/WebStatistic/GetStatistics", request);
        }
    }
}