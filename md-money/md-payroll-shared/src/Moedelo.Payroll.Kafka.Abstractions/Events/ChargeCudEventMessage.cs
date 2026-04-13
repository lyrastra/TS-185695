using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Payroll.Shared.Enums.Charge;

namespace Moedelo.Payroll.Kafka.Abstractions.Events
{
    public class ChargeCudEventMessage : IEntityEventData
    {
        public long Id { get; set; }

        public int FirmId { get; set; }

        public int UserId { get; set; }

        public ChargeEntityType ChargeType { get; set; }

        public int WorkerId { get; set; }

        public EventCudType EventType { get; set; }
    }
}