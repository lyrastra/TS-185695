using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Purchases;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.Upds.Events
{
    /// <summary>
    /// УПД (Покупки): удален
    /// </summary>
    public class PurchaseUpdDeleted : IEntityEventData
    {
        public PurchaseUpdDeletedState State { get; set; }
    }
}