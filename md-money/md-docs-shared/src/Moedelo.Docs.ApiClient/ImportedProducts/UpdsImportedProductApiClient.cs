using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.ImportedProducts;
using Moedelo.Docs.ApiClient.Abstractions.ImportedProducts.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.ImportedProducts
{
    [InjectAsSingleton(typeof(IUpdsImportedProductApiClient))]
    public class UpdsImportedProductApiClient : BaseApiClient, IUpdsImportedProductApiClient
    {
        public UpdsImportedProductApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<UpdsImportedProductApiClient> logger)
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

        public async Task<List<ImportedProductDto>> GetAllDeclarationsForProductAsync(long stockProductId)
        {
            var data = await GetAsync<DataResponse<List<ImportedProductDto>>>(
                "/private/api/v1/ImportedProducts/GetAllDeclarationsForProduct", new {stockProductId});
            return data.Data;
        }
    }
}