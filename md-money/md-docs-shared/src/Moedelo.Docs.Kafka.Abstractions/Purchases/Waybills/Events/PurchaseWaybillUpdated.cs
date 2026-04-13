using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Purchases.Events;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.Waybills.Events
{
    public class PurchaseWaybillUpdated : IEntityEventData
    {
        public PurchaseWaybillNewState State { get; set; }
    }
}