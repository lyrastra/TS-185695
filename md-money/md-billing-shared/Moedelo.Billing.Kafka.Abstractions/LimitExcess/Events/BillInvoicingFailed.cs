using Moedelo.Billing.Shared.Enums.InvoiceBill;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Billing.Kafka.Abstractions.LimitExcess.Events
{
    /// <summary>
    /// Событие "Ошибка выставления счёта"
    /// </summary>
    public class BillInvoicingFailed : IEntityEventData
    {
        public int FirmId { get; set; }

        public FailReason Reason { get; set; }

        public string Message { get; set; }
    }
}