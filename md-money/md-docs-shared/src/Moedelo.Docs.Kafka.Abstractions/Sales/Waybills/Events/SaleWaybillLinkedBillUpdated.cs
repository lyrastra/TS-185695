using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Waybills.Events
{
    /// <summary>
    /// Накладная (Продажи): обновлен счет
    /// </summary>
    public class SaleWaybillLinkedBillUpdated : IEntityEventData
    {
        public SaleWaybillLinkedBillState State { get; set; }
    }
}