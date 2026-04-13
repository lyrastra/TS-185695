using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.SalesUkd;
using Moedelo.Docs.ApiClient.Abstractions.SalesUkd.Model;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SalesUkd
{
    [InjectAsSingleton(typeof(IUkdSelfCostSourceApiClient))]
    public class UkdSelfCostSourceApiClient : BaseApiClient, IUkdSelfCostSourceApiClient
    {
        public UkdSelfCostSourceApiClient(IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SalesUkdApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("UkdApiEndpoint"),
                logger)
        {
        }

        public async Task<IReadOnlyCollection<SalesUkdSelfCostDto>> GetSalesOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct)
        {
            var response = await PostAsync<SelfCostSourceRequestDto, DataResponse<IReadOnlyCollection<SalesUkdSelfCostDto>>>(
                "/private/api/v1/SelfCostSources/GetOnDate/Sales",
                request,
                cancellationToken: ct);

            return response.Data;
        }
    }
}