using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Billing.Kafka.Abstractions.LimitExcess.Events
{
    /// <summary>
    /// Событие "Счёт выставлен"
    /// </summary>
    public class BillInvoiced : IEntityEventData
    {
        public int FirmId { get; set; }
        public string BillNumber { get; set; }
    }
}