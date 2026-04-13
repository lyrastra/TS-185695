using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Purchases;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.Upds.Events
{
    /// <summary>
    /// УПД (Покупки): обновлен
    /// </summary>
    public class PurchaseUpdUpdated : IEntityEventData
    {
        public PurchaseUpdNewState State { get; set; }
    }
}