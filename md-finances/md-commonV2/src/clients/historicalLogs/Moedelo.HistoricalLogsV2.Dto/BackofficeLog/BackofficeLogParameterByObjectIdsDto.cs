using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.HistoricalLogs.Backoffice;

namespace Moedelo.HistoricalLogsV2.Dto.BackofficeLog
{
    /// <summary> Параметр для извлечения списка действий пользователей над определенными объектами в партнерке </summary>
    public class BackofficeLogParameterByObjectIdsDto
    {
        /// <summary>Идентификаторы объектов, над которым произошло действие</summary>
        public List<int> ObjectIds { get; set; }

        /// <summary>Идентификатор пользователя</summary>
        public int? UserId { get; set; }

        /// <summary>Типы действий пользователя</summary>
        public List<BackofficeLogActionType> ActionTypes { get; set; }

        /// <summary>Успешность</summary>
        public bool? IsSuccess { get; set; }

        /// <summary>Минимальная дата создания записи</summary>
        public DateTime? MinCreateDate { get; set; }

        /// <summary>Максимальная дата создания записи</summary>
        public DateTime? MaxCreateDate { get; set; }

        /// <summary>Флаг, определяющий извлекать ли дополнительную информацию о логе</summary>
        public bool IsExractActionData { get; set; }

        /// <summary>Смещение</summary>
        public int Offset { get; set; }

        /// <summary>Количество</summary>
        public int Size { get; set; }
    }
}