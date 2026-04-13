using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.Calendar.Events
{
    public class CommonCalendarEventChanged : IEntityEventData
    {
        public int Id { get; set; }

        public string Type { get; set; }
        
        public int ModifyUserId { get; set; }
    }
}
