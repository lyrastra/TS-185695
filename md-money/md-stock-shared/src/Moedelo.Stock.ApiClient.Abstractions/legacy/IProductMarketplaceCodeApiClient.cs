using Moedelo.Common.Types;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Stock.Enums;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    public interface IProductMarketplaceCodeApiClient
    {
        /// <summary>
        /// Возвращает коды маркетплейса (МП) товара по фильтрам из параметров
        /// <param name="productIds">Фильтр по списку товаров</param>
        /// <param name="marketplaceType">фильтр по типу МП</param>
        /// <param name="onlyPrimaryCodeType">только записи с "основным типом кода для МП" (опционально)</param>
        /// </summary>
        Task<ProductMarketplaceCodeDto[]> GetByProductIdAndMarketplaceTypeAsync(
            FirmId firmId,
            UserId userId, 
            IReadOnlyCollection<long> productIds,
            MarketplaceType marketplaceType,
            bool onlyPrimaryCodeType = false);

        /// <summary>
        /// Возвращает коды маркетплейса товара по идентификаторам товаров
        /// </summary>
        Task<ProductMarketplaceCodeDto[]> GetByProductIdsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> productIds);
        
        /// <summary>
        /// Возвращает коды маркетплейсов всех товаров фирмы (для интеграции с "Финансист")
        /// </summary>
        Task<List<ProductMarketplaceCodeDto>> GetAllAsync(FirmId firmId, UserId userId);
    }
}