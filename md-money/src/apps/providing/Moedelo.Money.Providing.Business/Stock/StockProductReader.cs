using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Providing.Business.Stock.Models;
using Moedelo.Stock.ApiClient.Abstractions.legacy;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.Stock
{
    [InjectAsSingleton(typeof(StockProductReader))]
    class StockProductReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IProductApiClient productClient;

        public StockProductReader(
            IExecutionInfoContextAccessor contextAccessor,
            IProductApiClient productClient)
        {
            this.contextAccessor = contextAccessor;
            this.productClient = productClient;
        }

        public async Task<StockProduct[]> GetByIdsAsync(IReadOnlyCollection<long> productIds)
        {
            if (productIds.Count == 0)
            {
                return Array.Empty<StockProduct>();
            }
            var context = contextAccessor.ExecutionInfoContext;
            var products = await productClient.GetByIdsAsync(context.FirmId, context.UserId, productIds);
            return products.Select(Map).ToArray();
        }

        private static StockProduct Map(StockProductDto dto)
        {
            return new StockProduct
            {
                Id = dto.Id,
                Type = dto.Type
            };
        }
    }
}
