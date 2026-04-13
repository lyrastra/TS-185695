using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.AdvanceStatements;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.AdvanceStatements.Models;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SelfCostSources.AdvanceStatements
{
    [InjectAsSingleton(typeof(IAdvanceStatementSelfCostSourcesApiClient))]
    public class AdvanceStatementsSelfCostSourcesApiClient : BaseApiClient, IAdvanceStatementSelfCostSourcesApiClient
    {
        public AdvanceStatementsSelfCostSourcesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AdvanceStatementsSelfCostSourcesApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("AdvanceStatementsApiEndpoint"),
                logger)
        {
        }

        public async Task<IReadOnlyCollection<AdvanceStatementSelfCostDto>> GetOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct)
        {
            const string url = "/private/api/v1/SelfCostSources/GetOnDate";
            var result = await PostAsync<SelfCostSourceRequestDto, DataResponse<IReadOnlyCollection<AdvanceStatementSelfCostDto>>>(
                url,
                request,
                cancellationToken: ct);
            return result.Data;
        }
    }
}