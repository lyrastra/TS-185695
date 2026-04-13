using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Stocks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Client.ResponseWrappers;
using Moedelo.StockV2.Dto;
using Moedelo.StockV2.Dto.Products;

namespace Moedelo.StockV2.Client.Products
{
    [InjectAsSingleton]
    public class StockProductClient : BaseApiClient, IStockProductClient
    {
        private readonly SettingValue apiEndPoint;

        public StockProductClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) :
            base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("StockServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<List<StockProductDto>> GetProductsAsync(
            int firmId,
            int userId,
            ICollection<long> productIds)
        {
            if (productIds == null || !productIds.Any())
            {
                return new List<StockProductDto>();
            }

            var response = await PostAsync<ICollection<long>, ListResponse<StockProductDto>>(
                $"/Product/GetProducts?firmId={firmId}&userId={userId}",
                productIds).ConfigureAwait(false);

            return response.Items;
        }

        public Task<List<StockProductDto>> GetByAsync(
            int firmId,
            int userId,
            ProductSearchCriteriaDto searchCriteria)
        {
            return PostAsync<ProductSearchCriteriaDto, List<StockProductDto>>(
                $"/Product/ByCriteria?firmId={firmId}&userId={userId}",
                searchCriteria);
        }

        public async Task<List<string>> GetProductBarcodesAsync(
            int firmId,
            int userId,
            long productId
            )
        {
            return await GetAsync<List<string>>($"/Barcode/GetGoodBarcodes?firmId={firmId}&userId={userId}&productId={productId}").ConfigureAwait(false);
        }
        public async Task<List<BarcodesDto>> GetProductsBarcodesByProductIdListAsync(
            int firmId,
            int userId,
            ICollection<long> productIds)
        {
            if (productIds == null || !productIds.Any())
            {
                return new List<BarcodesDto>();
            }

            var response = await PostAsync<ICollection<long>, List<BarcodesDto>>(
                $"/Barcode/GetByProductIdList?firmId={firmId}&userId={userId}",
                productIds).ConfigureAwait(false);

            return response;
        }

        public Task<List<ProductForEvotorDto>> GetProductsForEvotorAsync(int firmId, int userId)
        {
            return GetAsync<List<ProductForEvotorDto>>(
                "/Product/GetProductsForEvotor",
                new { firmId, userId },
                null,
                new HttpQuerySetting
                {
                    Timeout = TimeSpan.FromMinutes(5)
                });
        }

        public Task SaveBarcodesAsync(int firmId, int userId, List<string> barcodes, long productId)
        {
            return PostAsync($"/Barcode/SaveBarcodes?firmId={firmId}&userId={userId}", new BarcodesDto
            {
                Barcodes = barcodes,
                ProductId = productId
            });
        }

        public Task<List<StockProductDto>> GetStockCatalogAsync(int firmId, int userId, string query, int offset, int count)
        {
            return GetAsync<List<StockProductDto>>("/Product/StockCatalog", new {firmId, userId, query, offset, count}, null,
                new HttpQuerySetting
                {
                    Timeout = TimeSpan.FromMinutes(10)
                });
        }

        public async Task<long> SaveAsync(int firmId, int userId, StockProductDto product)
        {
            var response = await PostAsync<StockProductDto, SavedLongId>($"/Product/Save?firmId={firmId}&userId={userId}", product).ConfigureAwait(false);
            return response.SavedId.Value;
        }

        public async Task<List<StockProductDto>> GetBySubcontoAsync(int firmId, int userId, IReadOnlyCollection<long> subcontoIds)
        {
            if (subcontoIds?.Any() != true)
            {
                return new List<StockProductDto>();
            }

            var response = await PostAsync<IEnumerable<long>, ListResponse<StockProductDto>>(
                $"/Product/GetBySubcontoIds?firmId={firmId}&userId={userId}",
                subcontoIds).ConfigureAwait(false);

            return response.Items;
        }

        public Task<ListWithCount<ProductBalanceDto>> GetProductBalanceAsync(int firmId, int offset, int count,
            string query, long? stockId = null, StockProductTypeEnum? stockProductType = null)
        {
            return GetAsync<ListWithCount<ProductBalanceDto>>("/Product/GetProductBalance", new { firmId, offset, count, query, stockId, stockProductType });
        }

        public Task<List<ProductCountInfoDto>> GetProductCountByStocksAsync(
            int firmId,
            int userId,
            IReadOnlyCollection<long> productIds)
        {
            return PostAsync<IEnumerable<long>, List<ProductCountInfoDto>>(
                $"/Product/CountByStocks?firmId={firmId}&userId={userId}",
                productIds);
        }

        public Task<CheckProductExistenceResultDto> IsExists(int firmId, int userId, CheckProductExistenceDto model)
        {
            return PostAsync<CheckProductExistenceDto, CheckProductExistenceResultDto>($"/Product/IsExists?firmId={firmId}&userId={userId}", model);
        }

        public Task<long?> SearchByNameArticleUnitAndType(int firmId, int userId, SearchByNameArticleUnitAndTypeDto model)
        {
            return PostAsync<SearchByNameArticleUnitAndTypeDto, long?>($"/Product/SearchByNameArticleUnitAndType?firmId={firmId}&userId={userId}", model);
        }

        public async Task<bool> IsStockRemainsEnteredAsync(int firmId, int userId, int year)
        {
            var result = await GetAsync<DataResponse<bool>>("/Product/IsStockRemainsEntered",
                new { firmId, userId, year }).ConfigureAwait(false);

            return result.Data;
        }

        public Task ResaveWithSubcontoAsync(int firmId, int userId)
        {
            return PostAsync("/Product/ResaveWithSubconto", new { firmId, userId });
        }

        public Task<bool> HasRawMaterialsAsync(int firmId, int userId)
        {
            return GetAsync<bool>("/Product/HasRawMaterials", new { firmId, userId });
        }

        public Task<List<ShortStockProductDto>> GetShortProductModelsAsync(int firmId, int userId)
        {
            return GetAsync<List<ShortStockProductDto>> ("/Product/GetShortProductModels", new { firmId, userId });
        }

        public Task<List<ShortStockProductDto>> CreateMultipleAsync(int firmId, int userId, IReadOnlyCollection<StockProductDto> products)
        {
            return PostAsync<IReadOnlyCollection <StockProductDto>, List <ShortStockProductDto>>(
                            $"/Product/CreateMultiple?firmId={firmId}&userId={userId}",
                            products);
        }
    }
}