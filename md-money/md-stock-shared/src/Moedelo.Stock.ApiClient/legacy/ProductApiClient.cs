using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Infrastructure.System.Extensions.Collections;
using Moedelo.Stock.ApiClient.Abstractions.legacy;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.StockOperations;
using Moedelo.Stock.ApiClient.legacy.models;

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IProductApiClient))]
    internal sealed class ProductApiClient : BaseLegacyApiClient, IProductApiClient
    {
        public ProductApiClient(
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

        public Task<List<StockProductDto>> GetByAsync(FirmId firmId, UserId userId, ProductSearchCriteriaDto dto)
        {
            if (dto == null)
            {
                return Task.FromResult(new List<StockProductDto>());
            }

            var uri = $"/Product/ByCriteria?firmId={firmId}&userId={userId}";

            return PostAsync<ProductSearchCriteriaDto, List<StockProductDto>>(uri, dto);
        }

        public async Task<List<StockProductDto>> GetByIdsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> productIds)
        {
            if (productIds == null || productIds.Count == 0)
            {
                return new List<StockProductDto>();
            }

            var uri = $"/Product/GetProducts?firmId={firmId}&userId={userId}";

            var response =
                await PostAsync<IReadOnlyCollection<long>, ListResult<StockProductDto>>(uri,
                    productIds.ToDistinctReadOnlyCollection()).ConfigureAwait(false);

            return response.Items;
        }

        public Task<List<ProductCountInfoDto>> GetProductCountByStocksAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> productIds)
        {
            if (productIds == null || productIds.Count == 0)
            {
                return Task.FromResult(new List<ProductCountInfoDto>());
            }

            var uri = $"/Product/CountByStocks?firmId={firmId}&userId={userId}";

            return PostAsync<IReadOnlyCollection<long>, List<ProductCountInfoDto>>(uri,
                productIds.ToDistinctReadOnlyCollection());
        }

        public Task<List<ShortStockProductDto>> GetShortProductModelsAsync(FirmId firmId, UserId userId)
        {
            return GetAsync<List<ShortStockProductDto>>("/Product/GetShortProductModels", new { firmId, userId });
        }

        public async Task<long> SaveAsync(FirmId firmId, UserId userId, StockProductDto product)
        {
            var response = await PostAsync<StockProductDto, SavedLongId>($"/Product/Save?firmId={firmId}&userId={userId}", product).ConfigureAwait(false);
            return response.SavedId.Value;
        }

        public Task SaveMarketplaceCodesAsync(FirmId firmId, UserId userId, MarketplaceCodesSaveRequestDto request)
        {
            if (request == null || request.MarketplaceCodes == null || request.ProductId <= 0 || !request.MarketplaceCodes.Any())
                return Task.CompletedTask;

            return PostAsync($"/Product/SaveMarketplaceCodes?firmId={firmId}&userId={userId}&productId={request.ProductId}", request.MarketplaceCodes);
        }

        public Task SaveForMarketplaceAsync(FirmId firmId, UserId userId, SaveForMarketplaceRequestDto request)
        {
            if (request == null || request.ProductId <= 0 ||
                (!request.MarketplaceCodes.Any() && string.IsNullOrWhiteSpace(request.Article)))
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/Product/SaveForMarketplace?firmId={firmId}&userId={userId}&productId={request.ProductId}&article={request.Article}", request.MarketplaceCodes);
        }


        public Task<List<ShortStockProductDto>> CreateMultipleAsync(FirmId firmId, UserId userId, IReadOnlyCollection<StockProductDto> products)
        {
            if (products == null || products.Count == 0)
            {
                return Task.FromResult(new List<ShortStockProductDto>());
            }

            var uri = $"/Product/CreateMultiple?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<StockProductDto>, List<ShortStockProductDto>>(uri, products);
        }

        public Task<List<StockProductForImportDto>> GetForImportAsync(FirmId firmId, UserId userId, MarketplaceRequestDto dto, HttpQuerySetting setting = null)
        {
            if (dto == null)
            {
                return Task.FromResult(new List<StockProductForImportDto>());
            }

            var uri = $"/Product/GetForImport?firmId={firmId}&userId={userId}";

            return PostAsync<MarketplaceRequestDto, List<StockProductForImportDto>>(uri, dto, setting: setting);
        }

        public Task<List<ImportedStockProductDto>> GetImportedFromMarketplaceAsync(FirmId firmId, UserId userId, MarketplaceRequestDto dto, HttpQuerySetting setting = null)
        {
            var uri = $"/Product/GetImportedFromMarketplace?firmId={firmId}&userId={userId}";

            return PostAsync<MarketplaceRequestDto, List<ImportedStockProductDto>>(uri, dto, setting: setting);
        }
    }
}