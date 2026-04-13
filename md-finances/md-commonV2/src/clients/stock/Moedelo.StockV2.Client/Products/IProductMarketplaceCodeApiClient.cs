using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Marketplaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.StockV2.Dto.Products;

namespace Moedelo.StockV2.Client.Products
{
    public interface IProductMarketplaceCodeApiClient : IDI
    {
        /// <summary>
        /// Возвращает коды маркетплейса (МП) товара по фильтрам из параметров
        /// <param name="productIds">Фильтр по списку товаров</param>
        /// <param name="marketplaceType">фильтр по типу МП</param>
        /// <param name="onlyPrimaryCodeType">только записи с "основным типом кода для МП" (опционально)</param>
        /// </summary>
        Task<ProductMarketplaceCodeDto[]> GetByProductIdAndMarketplaceTypeAsync(
            int firmId,
            int userId,
            IReadOnlyCollection<long> productIds,
            MarketplaceType marketplaceType,
            bool onlyPrimaryCodeType = false);
    }
}