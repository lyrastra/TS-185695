using System;
using Moedelo.Common.Enums.Enums.HistoricalLogs.Backoffice;

namespace Moedelo.HistoricalLogsV2.Dto.BackofficeLog
{
    /// <summary> Лог действия пользователя в партнерке</summary>
    public class BackofficeLogDto
    {
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }

        /// <summary>Идентификатор пользователя</summary>
        public int UserId { get; set; }

        /// <summary>Идентификатор объекта, над которым произошло действие</summary>
        public int? ObjectId { get; set; }

        /// <summary>Тип действия пользователя</summary>
        public BackofficeLogActionType ActionType { get; set; }

        /// <summary>Успешность</summary>
        public bool IsSuccess { get; set; }

        /// <summary>Дата создания записи</summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>Дополнительная информация о логе</summary>
        public BackofficeLogActionDataDto ActionData { get; set; }
    }
}