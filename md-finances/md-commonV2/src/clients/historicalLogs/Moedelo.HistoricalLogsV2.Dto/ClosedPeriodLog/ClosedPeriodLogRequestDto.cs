using System;
using Moedelo.Common.Enums.Enums.HistoricalLogs.ClosedPeriodLog;

namespace Moedelo.HistoricalLogsV2.Dto.ClosedPeriodLog
{
    public class ClosedPeriodLogRequestDto
    {
        /// <summary>
        /// Идентификатор фирмы, для которой открыли/закрыли период
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Кто открыл/закрыл период
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Начало периода
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Конец периода
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Тип события: 0 - закрытие, 1 - открытие
        /// </summary>
        public ClosedPeriodEventType EventType { get; set; }

        /// <summary>
        /// Когда произошло событие
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
