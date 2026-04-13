using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.Firm.Events
{
    public class UserLoginChanged : IEntityEventData
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
    }
}