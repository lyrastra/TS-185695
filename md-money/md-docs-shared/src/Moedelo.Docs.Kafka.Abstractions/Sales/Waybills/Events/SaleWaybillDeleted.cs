using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Waybills.Events
{
    /// <summary>
    /// Накладная (Продажи) удалена
    /// </summary>
    public class SaleWaybillDeleted : IEntityEventData
    {
        public SaleWaybillDeletedState State { get; set; }
    }
}