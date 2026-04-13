using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Waybills.Events
{
    /// <summary>
    /// Накладная (Продажи): обновлены платежи
    /// </summary>
    public class SaleWaybillPaymentsUpdated : IEntityEventData
    {
        public SaleWaybillPaymentsUpdatedState State { get; set; }
    }
}