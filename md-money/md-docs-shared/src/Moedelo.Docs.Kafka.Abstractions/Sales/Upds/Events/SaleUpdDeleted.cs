using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Sales;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Upds.Events
{
    /// <summary>
    /// УПД (Прдажи): удален
    /// </summary>
    public class SaleUpdDeleted : IEntityEventData
    {
        public SaleUpdDeletedState State { get; set; }
    }
}