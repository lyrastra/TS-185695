using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.StockVisibility.Events
{
    public class StockVisibilityChanged : IEntityEventData
    {
        public int FirmId { get; set; }
        public bool IsVisible { get; set; }
    }
}
