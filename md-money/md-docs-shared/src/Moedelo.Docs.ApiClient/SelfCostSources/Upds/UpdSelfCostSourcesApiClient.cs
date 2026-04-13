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
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Upds;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Upds.Models.Purchases;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Upds.Models.Sales;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SelfCostSources.Upds
{
    [InjectAsSingleton(typeof(IUpdSelfCostSourcesApiClient))]
    public class UpdSelfCostSourcesApiClient : BaseApiClient, IUpdSelfCostSourcesApiClient
    {
        public UpdSelfCostSourcesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<UpdSelfCostSourcesApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("UpdsApiEndpoint"),
                logger)
        {
        }

        public async Task<IReadOnlyCollection<PurchaseUpdSelfCostDto>> GetPurchasesOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct)
        {
            const string url = "/private/api/v1/SelfCostSources/GetOnDate/Purchases";
            var result = await PostAsync<SelfCostSourceRequestDto, DataResponse<IReadOnlyCollection<PurchaseUpdSelfCostDto>>> (
                url,
                request,
                cancellationToken: ct);
            return result.Data;
        }

        public async Task<IReadOnlyCollection<SaleUpdSelfCostDto>> GetSalesOnDateAsync(SelfCostSourceRequestDto request,
            CancellationToken ct)
        {
            const string url = "/private/api/v1/SelfCostSources/GetOnDate/Sales";
            var result = await PostAsync<SelfCostSourceRequestDto, DataResponse<IReadOnlyCollection<SaleUpdSelfCostDto>>> (
                url,
                request,
                cancellationToken: ct);
            return result.Data;
        }
    }
}