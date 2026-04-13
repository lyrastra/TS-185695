using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Billing.Kafka.Abstractions.Payments.Events
{
    public class PaymentHistoryTransferredEventData : IEntityEventData
    {
       public int FromFirmId { get; set; }
       
       public int ToFirmId { get; set; }
    }
}