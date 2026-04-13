using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Sales;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Upds.Events
{
    /// <summary>
    /// УПД (Прдажи): обновлен
    /// </summary>
    public class SaleUpdSignStatusUpdated : IEntityEventData
    {
        public SaleUpdSignStatusState State { get; set; }
    }
}