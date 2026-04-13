using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Waybills.Events
{
    /// <summary>
    /// Накладная (Продажи): обновлена
    /// </summary>
    public class SaleWaybillUpdated : IEntityEventData
    {
        public SaleWaybillNewState State { get; set; }
    }
}