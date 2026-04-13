using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Waybills.Events
{
    /// <summary>
    /// Накладная (Продажи): обновлен признак "Подписан"
    /// </summary>
    public class SaleWaybillSignStatusUpdated : IEntityEventData
    {
        public SaleWaybillSignStatusState State { get; set; }
    }
}