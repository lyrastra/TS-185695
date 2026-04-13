using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Stock.Enums;

namespace Moedelo.Stock.Kafka.Abstractions.Products.Events
{
    public class StockProductUpdatedMessage : IEntityEventData
    {
        public long StockProductId { get; set; }

        public string Name { get; set; }

        public StockProductTypeEnum Type { get; set; }

        public StockProductSubTypeEnum SubType { get; set; }

        public MaterialAccountCode SyntheticAccountCode { get; set; }

        public string Unit { get; set; }
    }
}
