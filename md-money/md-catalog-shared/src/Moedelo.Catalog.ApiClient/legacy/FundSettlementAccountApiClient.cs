using Microsoft.Extensions.Logging;
using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading.Tasks;

namespace Moedelo.Catalog.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IFundSettlementAccountApiClient))]
    class FundSettlementAccountApiClient : BaseLegacyApiClient, IFundSettlementAccountApiClient
    {
        public FundSettlementAccountApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FundSettlementAccountApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("CatalogApiEndpoint"),
                logger)
        {
        }

        public Task<string> MatchAsync(string settlementAccount)
        {
            return GetAsync<string>("/FundSettlementAccount/Match", new { settlementAccount });
        }
    }
}
