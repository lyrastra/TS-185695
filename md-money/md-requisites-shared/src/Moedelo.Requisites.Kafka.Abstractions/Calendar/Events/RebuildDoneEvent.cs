using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.Calendar.Events
{
    public class RebuildDoneEvent : IEntityEventData
    {
        public int FirmId { get; set; }
    }
}