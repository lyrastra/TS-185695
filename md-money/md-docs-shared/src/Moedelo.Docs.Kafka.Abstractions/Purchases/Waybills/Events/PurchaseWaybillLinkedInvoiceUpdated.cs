using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Purchases.Events;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.Waybills.Events
{
    /// <summary>
    /// Накладная (Покупки): обновлен сч-ф
    /// </summary>
    public class PurchaseWaybillLinkedInvoiceUpdated : IEntityEventData
    {
        public PurchaseWaybillLinkedInvoiceState State { get; set; }
    }
}