using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Sales;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Upds.Events
{
    /// <summary>
    /// УПД (Прдажи): создан
    /// </summary>
    public class SaleUpdCreated : IEntityEventData
    {
        public SaleUpdNewState State { get; set; }
    }
}