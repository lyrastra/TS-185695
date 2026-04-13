using System;

namespace Moedelo.HistoricalLogsV2.Dto.BackofficeLog
{
    /// <summary> Дополнительная информация о логе </summary>
    public class BackofficeLogActionDataDto
    {
        /// <summary>IP-адрес пользователя в момент совершения действия</summary>
        public string UserIpAddress { get; set; }

        /// <summary>Текст ошибки</summary>
        public string ErrorText { get; set; }

        /// <summary>Дата начала действия</summary>
        public DateTime? Start { get; set; }

        /// <summary>Дата окончания действия</summary>
        public DateTime? End { get; set; }

        /// <summary>Продолжительность действия</summary>
        public TimeSpan Duration => Start.HasValue && End.HasValue ? End.Value.Subtract(Start.Value) : TimeSpan.Zero;

        /// <summary>Набор произвольных параметров</summary>
        public string Parameters { get; set; }
    }
}