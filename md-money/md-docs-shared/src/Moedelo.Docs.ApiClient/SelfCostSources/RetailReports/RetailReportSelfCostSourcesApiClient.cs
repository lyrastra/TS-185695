using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.RetailReports;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.RetailReports.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Docs.ApiClient.SelfCostSources.RetailReports
{
    [InjectAsSingleton(typeof(IRetailReportSelfCostSourcesApiClient))]
    public class RetailReportSelfCostSourcesApiClient : BaseApiClient, IRetailReportSelfCostSourcesApiClient
    {
        public RetailReportSelfCostSourcesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<RetailReportSelfCostSourcesApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("RetailReportsApiEndpoint"),
                logger)
        {
        }

        public async Task<IReadOnlyCollection<RetailReportSelfCostDto>> GetOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct)
        {
            const string url = "/private/api/v1/SelfCostSources/GetOnDate";
            var result = await PostAsync<SelfCostSourceRequestDto, DataResponse<IReadOnlyCollection<RetailReportSelfCostDto>>>(
                url,
                request,
                setting: new HttpQuerySetting(new TimeSpan(0, 2, 0)),
                cancellationToken: ct);
            return result.Data;
        }
    }
}