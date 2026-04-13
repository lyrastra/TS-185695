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
using Moedelo.Infrastructure.System.Extensions.Collections;

namespace Moedelo.Catalog.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IFnsApiClient))]
    internal sealed class FnsApiClient : BaseLegacyApiClient, IFnsApiClient
    {
        public FnsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FnsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("CatalogApiEndpoint"),
                logger)
        {
        }

        public Task<FnsDto> GetByCodeAsync(string code) =>
            GetAsync<FnsDto>("/Fns/V2/GetByCode", new { code });

        public Task<List<FnsDto>> GetByRegionAsync(string regionCode) =>
            GetAsync<List<FnsDto>>("/Fns/V2/GetByRegion", new {regionCode});

        public Task<FnsRequisitesDto> GetRequisitesByCodeAndOktmoAsync(string code, string oktmo) =>
            GetAsync<FnsRequisitesDto>($"/Fns/V2/GetRequisitesByCodeAndOktmo?code={code}&oktmo={oktmo}");

        public Task<List<FnsDto>> GetByCodesAsync(IReadOnlyCollection<string> codes) =>
            codes.NullOrEmpty()
                ? Task.FromResult(new List<FnsDto>())
                : PostAsync<IReadOnlyCollection<string>, List<FnsDto>>("/Fns/V2/GetByCodes",
                    codes.ToDistinctReadOnlyCollection());
    }
}
