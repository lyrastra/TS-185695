using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.StockV2.Dto.Operations.ProductMerge;
using Moedelo.StockV2.Dto.StockOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.StockV2.Client.Orders
{
    /// <summary>
    /// Интерфейс к приватному контроллеру CustomerOrdersController в md-stockOrders
    /// </summary>
    public interface ICustomerOrdersClient : IDI
    {
        /// <summary>
        /// Возвращает список заказов от покупателя
        /// </summary>
        Task<List<CustomerOrderDto>> GetListAsync(int firmId, int userId);

        /// <summary>
        /// Объединияет номенклатуры в заказах от покупателя
        /// </summary>
        Task MergeItemsAsync(int firmId, int userId, ProductMergeRequestDto mergeRequest);

        /// <summary>
        /// Возвращает список заказов от покупателя, ссылающихся на указанные товары (нужно для валидатора удаления товаров) одним запросом.
        /// </summary>
        Task<List<OrderToProductReferenceResponse>> GetCustomerOrdersToProductsReferencesAsync(int firmId, int userId, OrdersToProductsReferencesRequest request);
    }
}