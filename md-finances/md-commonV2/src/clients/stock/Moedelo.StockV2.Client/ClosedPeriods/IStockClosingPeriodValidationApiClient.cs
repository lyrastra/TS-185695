using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.StockV2.Dto.ClosingPeriodValidations;
using Moedelo.StockV2.Dto.Products;

namespace Moedelo.StockV2.Client.ClosedPeriods
{
    /// <summary>
    /// Набор операций, необходимых при реализации функционала Мастера Закрытия Месяца (МЗМ) на стороне Stock
    /// </summary>
    public interface IStockClosingPeriodValidationApiClient : IDI
    {
        /// <summary>
        /// Непроведенные в учете складские документы за период
        /// </summary>
        Task<List<StockOperationsCountDto>> GetOperationsNonProvidedInAccountingAsync(
            int firmId, int userId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Получить инветраризации с зарегистрированным излишком комплектов
        /// </summary>
        Task<List<InventoryWithBundleIncomesDto>> GetInventoriesWithBundleIncomesAsync(int firmId, int userId, DateTime beforeDate);
        
        /// <summary>
        /// Получить сведения о недостатке товаров за период
        /// </summary>
        Task<List<ProductBalanceInfoDto>> GetStockProductNegativeBalancesAsync(int firmId, int userId,
            DateTime startDate, DateTime endDate);

        /// <summary>
        /// Получить отрицательное количество использования сырья при производстве готовой продукции и выписанного со склада
        /// с учетом остатков незавершенного производства за прошлый месяц
        /// </summary>
        Task<List<ProductBalanceInfoDto>> GetStockProductNegativeBalanceUsingMaterialsAsync(int firmId, int userId,
            DateTime startDate, DateTime endDate);

        /// <summary>
        /// Возвращает остатки товаров на дату
        /// </summary>
        Task<List<ProductBalanceInfoDto>> GetProductBalancesOnDateAsync(int firmId, int userId, ProductBalancesRequestDto request);
    }
}