using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Purchases;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.Upds.Events
{
    /// <summary>
    /// УПД (Покупки): обновлены вычеты НДС
    /// </summary>
    public class PurchaseUpdNdsDeductionsUpdated : IEntityEventData
    {
        public PurchaseUpdNdsDeductionsState State { get; set; }
    }
}