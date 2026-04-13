using Microsoft.Extensions.Logging;
using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.System.Extensions.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Catalog.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IBankIconApiClient))]
    public class BankIconApiClient : BaseLegacyApiClient, IBankIconApiClient
    {
        public BankIconApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BankIconApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("CatalogApiEndpoint"),
                logger)
        {
        }

        public Task<Dictionary<int, string>> GetByBankIdsAsync(IReadOnlyCollection<int> bankIds)
        {
            return bankIds.NullOrEmpty()
                ? Task.FromResult(new Dictionary<int, string>())
                : PostAsync<IReadOnlyCollection<int>, Dictionary<int, string>>("/BankIcon/V2/GetByBankIds",
                    bankIds.ToDistinctReadOnlyCollection());
        }
    }
}
