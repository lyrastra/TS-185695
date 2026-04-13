using System;

namespace Moedelo.HistoricalLogsV2.Dto
{
    public class TelemetryEventLogRequestDto
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public string EventName { get; set; }

        /// <summary>
        /// Время возникновения события.
        /// Если не указать, инициализируется текущим серверным временем
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// любые дополнительные данные
        /// желательно, чтобы это было что-то в формате json
        /// Максимальная длина - 900 символов
        /// </summary>
        public string EventBody { get; set; }
    }
}