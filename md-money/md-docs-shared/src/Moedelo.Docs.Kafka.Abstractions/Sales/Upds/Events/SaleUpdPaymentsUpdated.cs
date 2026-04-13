using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Sales;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Upds.Events
{
    /// <summary>
    /// УПД (Прдажи): обновлены платежи
    /// </summary>
    public class SaleUpdPaymentsUpdated : IEntityEventData
    {
        public SaleUpdPaymentsState State { get; set; }
    }
}