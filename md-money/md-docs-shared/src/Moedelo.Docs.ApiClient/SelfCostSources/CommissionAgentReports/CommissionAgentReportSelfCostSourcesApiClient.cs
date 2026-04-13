using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CommissionAgentReports;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CommissionAgentReports.Models;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SelfCostSources.CommissionAgentReports
{
    [InjectAsSingleton(typeof(ICommissionAgentReportSelfCostSourcesApiClient))]
    public class CommissionAgentReportSelfCostSourcesApiClient : BaseApiClient, ICommissionAgentReportSelfCostSourcesApiClient
    {
        public CommissionAgentReportSelfCostSourcesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<CommissionAgentReportSelfCostSourcesApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("CommissionAgentReportsApiEndpoint"),
                logger)
        {
        }

        public async Task<IReadOnlyCollection<CommissionAgentReportSelfCostDto>> GetOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct)
        {
            const string url = "/private/api/v1/SelfCostSources/GetOnDate";
            var result = await PostAsync<SelfCostSourceRequestDto, DataResponse<IReadOnlyCollection<CommissionAgentReportSelfCostDto>>>(
                url,
                request,
                cancellationToken: ct);
            return result.Data;
        }
    }
}