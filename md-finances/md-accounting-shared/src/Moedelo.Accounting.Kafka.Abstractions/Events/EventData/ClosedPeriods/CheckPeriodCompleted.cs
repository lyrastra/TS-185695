using System;
using System.Collections.Generic;
using Moedelo.Accounting.Enums.ClosedPeriods;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounting.Kafka.Abstractions.Events.EventData.ClosedPeriods
{
    /// <summary>
    /// Валидация при закрытии периода  
    /// </summary>
    public class CheckPeriodCompleted : IEntityEventData
    {
        /// <summary>
        /// Идентификатор события в календаре (для ссылки на МЗМ)
        /// </summary>
        public int CalendarId { get; set; }
        
        /// <summary>
        /// Начало проверяемого периода
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Конец проверяемого периода
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// Предупреждения (НЕ блокируют МЗМ)
        /// </summary>
        public List<ClosingPeriodCheckRule> Warnings { get; set; }
        
        /// <summary>
        /// Ошибки (блокируют МЗМ)
        /// </summary>
        public List<ClosingPeriodCheckRule> Errors { get; set; }

        /// <summary>
        /// Отрицательные БУ остатки
        /// </summary>
        public List<NegativeBalance> NegativeBalances { get; set; }
    }
}