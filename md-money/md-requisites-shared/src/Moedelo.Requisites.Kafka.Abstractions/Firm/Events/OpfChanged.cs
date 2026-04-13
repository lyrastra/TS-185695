using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Requisites.Enums.FirmRequisites;

namespace Moedelo.Requisites.Kafka.Abstractions.Firm.Events
{
    public class OpfChanged : IEntityEventData
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public Opf? Opf { get; set; }
        public bool? IsOoo { get; set; }
    }
}