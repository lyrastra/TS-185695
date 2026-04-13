using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.Firm.Events
{
    public class RequisitesChanged : IEntityEventData
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
    }
}