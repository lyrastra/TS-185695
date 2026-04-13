using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.StockV2.Dto.Operations.ProductMerge;
using Moedelo.StockV2.Dto.StockOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.StockV2.Client.Orders
{
    public interface ISupplierOrdersClient : IDI
    {
        /// <summary>
        /// Возвращает список заказов от поставщика
        /// </summary>
        Task<List<SupplierOrderDto>> GetListAsync(int firmId, int userId);

        /// <summary>
        /// Объединить номенклатуры в заказах поставщику
        /// </summary>
        Task MergeItemsAsync(int firmId, int userId, ProductMergeRequestDto mergeRequest);

        /// <summary>
        /// Получить список заказов поставщику, ссылающихся на указанные товары (нужно для валидатора удаления товаров) одним запросом.
        /// </summary>
        Task<List<OrderToProductReferenceResponse>> GetSupplierOrdersToProductsReferencesAsync(int firmId, int userId, OrdersToProductsReferencesRequest request);
    }
}