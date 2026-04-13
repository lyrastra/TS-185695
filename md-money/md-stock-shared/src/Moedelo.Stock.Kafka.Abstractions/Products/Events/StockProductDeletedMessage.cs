using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Stock.Kafka.Abstractions.Products.Events
{
    public class StockProductDeletedMessage : IEntityEventData
    {
        public long StockProductId { get; set; }
    }
}
