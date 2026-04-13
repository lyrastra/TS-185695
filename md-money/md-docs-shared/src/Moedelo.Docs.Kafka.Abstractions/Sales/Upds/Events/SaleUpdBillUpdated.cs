using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Sales;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Upds.Events
{
    /// <summary>
    /// УПД (Прдажи): обновлен счет
    /// </summary>
    public class SaleUpdBillUpdated : IEntityEventData
    {
        public SaleUpdBillState State { get; set; }
    }
}