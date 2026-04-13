using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.StockV2.Dto.Products;

namespace Moedelo.StockV2.Client.Products
{
    public interface IProductTypeCodeApiClient : IDI
    {
        /// <summary>
        /// Получение всех не устаревших кодов товаров из МД
        /// </summary>
        Task<ProductTypeCodeDto[]> GetAsync();

        /// <summary>
        /// Получение кодов по идентификаторам
        /// </summary>
        Task<ProductTypeCodeDto[]> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids);

        /// <summary>
        /// Добавление кодов в МД
        /// </summary>
        Task AddAsync(IReadOnlyCollection<ProductTypeCodeCreateDto> codes, HttpQuerySetting setting = null);

        /// <summary>
        /// Устанавливает коды устаревшими по идентификаторам
        /// </summary>
        Task SetOutdatedAsync(IReadOnlyCollection<int> ids);
    }
}