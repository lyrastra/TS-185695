using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounting.Kafka.Abstractions.Events.EventData.ClosedPeriods
{
    /// <summary>
    /// Период открыт (переоткрыт МЗМ)
    /// </summary>
    public class ClosedPeriodDeleted : IEntityEventData
    {
        /// <summary>
        /// Идентификатор события в календаре (для ссылки на 1-й МЗМ в открытом периоде)
        /// </summary>
        public int CalendarId { get; set; }
        
        /// <summary>
        /// Дата, с которой открыт месяц 
        /// </summary>
        public DateTime SinceDate { get; set; }
    }
}