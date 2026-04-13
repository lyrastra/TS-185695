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
    [InjectAsSingleton(typeof(IPfrApiClient))]
    internal sealed class PfrApiClient : BaseLegacyApiClient, IPfrApiClient
    {
        public PfrApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PfrApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("CatalogApiEndpoint"),
                logger)
        {
        }

        public Task<PfrDto> GetByCodeAsync(string code)
        {
            return GetAsync<PfrDto>("/Pfr/V2/GetDepartmentByCode", new { code });
        }
    }
}