using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.StockV2.Dto;
using Moedelo.StockV2.Dto.Products;

namespace Moedelo.StockV2.Client.Products
{
    public interface IProductComponentClient : IDI
    {
        /// <summary>
        /// Получить продукты по списку ids
        /// </summary>
        Task<List<ProductComponentDto>> GetAsync(int firmId, int userId, List<long> productIds);
    }
}