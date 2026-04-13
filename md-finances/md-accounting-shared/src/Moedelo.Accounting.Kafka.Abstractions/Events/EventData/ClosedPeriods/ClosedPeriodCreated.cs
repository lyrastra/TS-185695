using System;
using Moedelo.Accounting.Enums.ClosedPeriods;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounting.Kafka.Abstractions.Events.EventData.ClosedPeriods
{
    /// <summary>
    /// Период закрыт (завершен МЗМ)
    /// </summary>
    public class ClosedPeriodCreated : IEntityEventData
    {
        /// <summary>
        /// Идентификатор события в календаре (для ссылки на МЗМ)
        /// </summary>
        public int CalendarId { get; set; }
        
        /// <summary>
        /// Начало закрываемого периода
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Конец закрываемого периода
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Как был закрыт период
        /// </summary>
        public CloseType CloseType { get; set; }
    }
}