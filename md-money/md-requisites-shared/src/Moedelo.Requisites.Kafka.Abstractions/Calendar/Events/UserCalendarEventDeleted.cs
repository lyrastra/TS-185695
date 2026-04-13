using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.Calendar.Events
{
    public class UserCalendarEventDeleted : IEntityEventData
    {
        /// <summary>
        /// Идентификатор пользовательского события
        /// </summary>
        public int Id { get; set; }

        public int FirmId { get; set; }
    }
}
