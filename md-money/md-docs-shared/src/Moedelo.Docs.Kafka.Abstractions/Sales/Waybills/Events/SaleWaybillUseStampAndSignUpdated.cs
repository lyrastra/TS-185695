using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Waybills.Events
{
    /// <summary>
    /// Накладная (Продажи): обновлен признак "Печать и подпись"
    /// </summary>
    public class SaleWaybillUseStampAndSignUpdated : IEntityEventData
    {
        public SaleWaybillUseStampAndSignState State { get; set; }
    }
}