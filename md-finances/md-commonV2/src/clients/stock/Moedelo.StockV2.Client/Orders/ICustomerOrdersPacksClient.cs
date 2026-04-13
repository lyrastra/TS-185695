using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.StockV2.Dto.Operations.ProductMerge;
using Moedelo.StockV2.Dto.StockOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.StockV2.Client.Orders
{
    /// <summary>
    /// Интерфейс к приватному контроллеру CustomerOrdersPacksController в md-stockOrders
    /// </summary>
    public interface ICustomerOrdersPacksClient : IDI
    {
        /// <summary>
        /// Берёт все сборки для указанной фирмы и меняет в них товары/материалы secondaryProducts на primaryProduct. Названия товаров не трогает
        /// </summary>
        Task MergeItemsAsync(int firmId, int userId, ProductMergeRequestDto mergeRequest);

        /// <summary>
        /// Получить список сборок, ссылающихся на указанные товары (нужно для валидатора удаления товаров) одним запросом.
        /// </summary>
        Task<List<OrderToProductReferenceResponse>> GetOrderPacksToProductsReferencesAsync(int firmId, int userId, OrdersToProductsReferencesRequest request);
    }
}
