using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Waybills.Events
{
    /// <summary>
    /// Накладная (Продажи): обновлен сч-ф
    /// </summary>
    public class SaleWaybillLinkedInvoiceUpdated : IEntityEventData
    {
        public SaleWaybillLinkedInvoiceState State { get; set; }
    }
}