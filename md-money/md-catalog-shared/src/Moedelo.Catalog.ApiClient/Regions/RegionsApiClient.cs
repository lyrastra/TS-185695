using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Catalog.ApiClient.Abstractions.Regions;
using Moedelo.Catalog.ApiClient.Abstractions.Regions.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Catalog.ApiClient.Regions
{
    [InjectAsSingleton(typeof(IRegionApiClient))]
    internal sealed class RegionsApiClient : BaseApiClient, IRegionApiClient
    {
        private const string ApiRoute = "/Region/V2";

        public RegionsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<RegionsApiClient> logger,
            string auditTypeName = null) : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("CatalogApiEndpoint"), 
                logger,
                auditTypeName)
        {
        }

        public Task<RegionDto> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return GetAsync<RegionDto>(
                $"{ApiRoute}/GetByCode",
                new { code },
                cancellationToken: cancellationToken);
        }

        public Task<RegionDto> GetByIdAsync(
            int id, 
            CancellationToken cancellationToken = default)
        {
            return GetAsync<RegionDto>(
                $"{ApiRoute}/GetById", 
                new { id }, 
                cancellationToken: cancellationToken);
        }

        public Task<IReadOnlyCollection<RegionDto>> GetByIdsAsync(
            IReadOnlyCollection<int> ids, 
            CancellationToken cancellationToken = default)
        {
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<RegionDto>>(
                $"{ApiRoute}/GetByIds",
                ids,
                cancellationToken: cancellationToken);
        }

        public Task<RegionDto> GetByPhoneAsync(
            string phone, 
            CancellationToken cancellationToken = default)
        {
            return GetAsync<RegionDto>(
                $"{ApiRoute}/GetByPhone", 
                new { phone }, 
                cancellationToken: cancellationToken);
        }

        public Task<IReadOnlyDictionary<string, RegionDto>> GetByPhonesAsync(
            IReadOnlyCollection<string> phones,
            CancellationToken cancellationToken = default)
        {
            return PostAsync<IReadOnlyCollection<string>, IReadOnlyDictionary<string, RegionDto>>(
                $"{ApiRoute}/GetByPhones",
                phones,
                cancellationToken: cancellationToken);
        }
    }
}