using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Sales;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Upds.Events
{
    /// <summary>
    /// УПД (Прдажи): обновлен признак "Печать и подпись"
    /// </summary>
    public class SaleUpdStampAndSignUpdated : IEntityEventData
    {
        public SaleUpdStampAndSignState State { get; set; }
    }
}