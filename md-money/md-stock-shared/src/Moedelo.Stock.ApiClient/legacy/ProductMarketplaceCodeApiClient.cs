using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Stock.ApiClient.Abstractions.legacy;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Stock.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IProductMarketplaceCodeApiClient))]
    internal sealed class ProductMarketplaceCodeApiClient : BaseLegacyApiClient, IProductMarketplaceCodeApiClient
    {
        public ProductMarketplaceCodeApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ProductApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("StockApiEndpoint"),
                logger)
        {
        }

        public Task<ProductMarketplaceCodeDto[]> GetByProductIdAndMarketplaceTypeAsync(
            FirmId firmId,
            UserId userId,
            IReadOnlyCollection<long> productIds,
            MarketplaceType marketplaceType,
            bool onlyPrimaryCodeType = false)
        {
            return PostAsync<IReadOnlyCollection<long>, ProductMarketplaceCodeDto[]>(
                $"/ProductMarketplace/GetByProductIdsAndMarketplaceTypeAsync?firmId={firmId}&userId={userId}&type={(int)marketplaceType}&onlyPrimaryCodeType={onlyPrimaryCodeType}",
                productIds);
        }
        
        public Task<ProductMarketplaceCodeDto[]> GetByProductIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> productIds)
        {
            return PostAsync<IReadOnlyCollection<long>, ProductMarketplaceCodeDto[]>(
                $"/ProductMarketplace/GetByProductIdsAsync?firmId={firmId}&userId={userId}",
                productIds);
        }
        
        public Task<List<ProductMarketplaceCodeDto>> GetAllAsync(FirmId firmId, UserId userId)
        {
            return GetAsync<List<ProductMarketplaceCodeDto>>("/ProductMarketplace/Codes", new { firmId, userId });
        }
    }
}