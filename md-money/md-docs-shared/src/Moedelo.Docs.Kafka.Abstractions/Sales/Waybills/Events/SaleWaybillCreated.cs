using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Waybills.Events
{
    /// <summary>
    /// Накладная (Продажи): создана
    /// </summary>
    public class SaleWaybillCreated : IEntityEventData
    {
        public SaleWaybillNewState State { get; set; }
    }
}