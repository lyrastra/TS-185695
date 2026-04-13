using System.Collections.Generic;

namespace Moedelo.StockV2.Dto.StockOrders
{
    /// <summary>
    /// DTO-шка с запросом на получение заказов, ссылающихся на продукты.
    /// </summary>
    public class OrdersToProductsReferencesRequest
    {
        /// <summary>
        /// Для каких продуктов получаем заказы
        /// </summary>
        public IReadOnlyCollection<long> ProductIds { get; set; }
    }
}
