using System.Collections.Generic;
using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;

namespace Moedelo.Catalog.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IFssApiClient))]
    internal sealed class FssApiClient : BaseLegacyApiClient, IFssApiClient
    {
        public FssApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FssApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("CatalogApiEndpoint"),
                logger)
        {
        }

        public Task<FssDto> GetByCodeAsync(string code)
        {
            return GetAsync<FssDto>("/Fss/V2/GetByCode", new { code });
        }
        public Task<List<FssDto>> GetByRegionAsync(string regionCode)
        {
            return GetAsync<List<FssDto>>("/Fss/V2/GetByRegion", new { regionCode });
        }
    }
}