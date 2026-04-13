using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.StockV2.Dto.Products;

namespace Moedelo.StockV2.Client.Products
{
    public interface IProductSummaryClient : IDI
    {
        /// <summary>
        /// топ товарных позиций из раздела «Запасы», которые продавались лучше всего
        /// считается по количеству проданного
        /// </summary>
        Task<List<FirmProductSaleSummaryDto>> GetProductTopSalesAsync(IReadOnlyCollection<int> firmIds, int count, DateTime startDate, DateTime endDate);
    }
}