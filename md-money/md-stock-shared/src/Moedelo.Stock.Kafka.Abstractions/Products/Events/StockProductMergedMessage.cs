using System.Collections.Generic;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Stock.Kafka.Abstractions.Products.Events
{
    public class StockProductMergedMessage : IEntityEventData
    {
        /// <summary>
        /// Основной продукт с которым объеденили другие продукты
        /// </summary>
        public long PrimaryStockProductId { get; set; }

        /// <summary>
        /// Продукты которые объединили с основным продуктом
        /// </summary>
        public IReadOnlyCollection<long> SecondaryStockProductIds { get; set; }
    }
}
