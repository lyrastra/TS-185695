using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.StockOperations;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    public interface IProductApiClient
    {
        Task<List<StockProductDto>> GetByAsync(FirmId firmId, UserId userId, ProductSearchCriteriaDto dto);

        Task<List<StockProductDto>> GetByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> productIds);

        Task<List<ProductCountInfoDto>> GetProductCountByStocksAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> productIds);

        Task<long> SaveAsync(FirmId firmId, UserId userId, StockProductDto product);

        Task SaveMarketplaceCodesAsync(FirmId firmId, UserId userId, MarketplaceCodesSaveRequestDto product);

        /// <summary>
        /// Обновление полей товара при синхронизации с МП. Обновляются МП-коды и артикул
        /// </summary>
        Task SaveForMarketplaceAsync(FirmId firmId, UserId userId, SaveForMarketplaceRequestDto request);

        Task<List<ShortStockProductDto>> CreateMultipleAsync(FirmId firmId, UserId userId, IReadOnlyCollection<StockProductDto> products);

        /// <summary>
        /// Получить упрощённый список номенклатур (для интеграции с "Финансист")
        /// </summary>
        Task<List<ShortStockProductDto>> GetShortProductModelsAsync(FirmId firmId, UserId userId);

        Task<List<StockProductForImportDto>> GetForImportAsync(FirmId firmId, UserId userId, MarketplaceRequestDto dto, HttpQuerySetting setting = null);

        Task<List<ImportedStockProductDto>> GetImportedFromMarketplaceAsync(FirmId firmId, UserId userId, MarketplaceRequestDto dto, HttpQuerySetting setting = null);
    }
}