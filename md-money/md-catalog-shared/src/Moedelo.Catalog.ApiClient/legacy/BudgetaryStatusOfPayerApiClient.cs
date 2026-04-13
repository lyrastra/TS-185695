using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Catalog.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IBudgetaryStatusOfPayerApiClient))]
    internal sealed class BudgetaryStatusOfPayerApiClient : BaseLegacyApiClient, IBudgetaryStatusOfPayerApiClient
    {
        public BudgetaryStatusOfPayerApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BudgetaryStatusOfPayerApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("CatalogApiEndpoint"),
                logger)
        {
        }

        public Task<BudgetaryStatusOfPayerDto[]> GetListAsync() => GetAsync<BudgetaryStatusOfPayerDto[]>("/BudgetaryStatusOfPayer/V2/List");
    }
}
