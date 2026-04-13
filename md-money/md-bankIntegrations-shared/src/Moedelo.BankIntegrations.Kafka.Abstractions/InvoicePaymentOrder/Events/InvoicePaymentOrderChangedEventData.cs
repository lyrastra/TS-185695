using Moedelo.BankIntegrations.Enums.Invoices;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.BankIntegrations.Kafka.Abstractions.InvoicePaymentOrder.Events
{
    /// <summary> Событие об изменение прямого(сквозного) платежа </summary>
    public class InvoicePaymentOrderChangedEventData : IEntityEventData
    {
        public int FirmId { get; set; }
        public int InvoiceId { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public string InvoiceDescription { get; set; }
        public long DocumentBaseId { get; set; }
    }
}