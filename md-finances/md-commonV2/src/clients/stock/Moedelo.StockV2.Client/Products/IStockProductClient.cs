using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Stocks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.StockV2.Dto;
using Moedelo.StockV2.Dto.Products;
using Moedelo.StockV2.Dto.SelfCost;

namespace Moedelo.StockV2.Client.Products
{
    public interface IStockProductClient : IDI
    {
        /// <summary>
        /// Получить продукты по списку ids
        /// </summary>
        Task<List<StockProductDto>> GetProductsAsync(int firmId, int userId, ICollection<long> productIds);
        
        /// <summary>
        /// Найти товары по заданным параметрам 
        /// </summary>
        Task<List<StockProductDto>> GetByAsync(int firmId, int userId, ProductSearchCriteriaDto searchCriteria);

        /// <summary>Получить штрихкоды товара по Id</summary>
        Task<List<string>> GetProductBarcodesAsync(int firmId, int userId, long productId);

        /// <summary>Для Эвотора. Получить ВСЕ товары</summary>
        Task<List<ProductForEvotorDto>> GetProductsForEvotorAsync(int firmId, int userId);

        /// <summary>Сохранение штрих кодов товара</summary>
        Task SaveBarcodesAsync(int firmId, int userId, List<string> barcodes, long productId);

        Task<List<StockProductDto>> GetStockCatalogAsync(int firmId, int userId, string query, int offset, int count);

        /// <summary>
        /// Сохраняет продукт. Возвращает идентификатор товара
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="userId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<long> SaveAsync(int firmId, int userId, StockProductDto product);

        /// <summary>
        /// Массовое создание товаров.
        /// </summary>
        /// <returns>Неполные модели созданных товаров</returns>
        Task<List<ShortStockProductDto>> CreateMultipleAsync(int firmId, int userId, IReadOnlyCollection<StockProductDto> products);

        /// <summary>
        /// Получить товары по списку идентификаторов субконто
        /// </summary>
        Task<List<StockProductDto>> GetBySubcontoAsync(int firmId, int userId, IReadOnlyCollection<long> subcontoIds);

        /// <summary>
        /// Получить остатки товаров по складам
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="query"></param>
        /// <param name="stockId"></param>
        /// <param name="productType"></param>
        /// <returns></returns>
        Task<ListWithCount<ProductBalanceDto>> GetProductBalanceAsync(int firmId, int offset, int count, string query,
            long? stockId = null, StockProductTypeEnum? stockProductType = null);

        
        /// <summary>
        /// Получить остатки товаров по складам
        /// </summary>
        Task<List<ProductCountInfoDto>> GetProductCountByStocksAsync(int firmId, int userId, IReadOnlyCollection<long> productIds);

        /// <summary>
        /// Получить штрихкоды по списку id товаров
        /// </summary>
        Task<List<BarcodesDto>> GetProductsBarcodesByProductIdListAsync(
            int firmId,
            int userId,
            ICollection<long> productIds);

        /// <summary>
        /// Проверить существование продукта по переданным параметрам.
        /// </summary>
        Task<CheckProductExistenceResultDto> IsExists(int firmId, int userId, CheckProductExistenceDto model);

        Task<long?> SearchByNameArticleUnitAndType(int firmId, int userId, SearchByNameArticleUnitAndTypeDto model);

        Task<bool> IsStockRemainsEnteredAsync(int firmId, int userId, int year);

        Task ResaveWithSubcontoAsync(int firmId, int userId);

        Task<bool> HasRawMaterialsAsync(int firmId, int userId);

        /// <summary>
        /// Получить упрощённый список номенклатур (для интеграции с "Финансист")
        /// </summary>
        Task<List<ShortStockProductDto>> GetShortProductModelsAsync(int firmId, int userId);
    }
}