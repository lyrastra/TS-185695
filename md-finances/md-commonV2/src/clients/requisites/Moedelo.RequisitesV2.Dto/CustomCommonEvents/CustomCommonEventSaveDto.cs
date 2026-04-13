using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Calendar;

namespace Moedelo.RequisitesV2.Dto.CustomCommonEvents
{
    public class CustomCommonEventSaveDto
    {
        /// <summary>
        /// Название события
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание события
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Тип ссылки, на которое ведёт кастомное событие
        /// </summary>
        public UrlType UrlType { get; set; }

        /// <summary>
        /// Ссылка, на которое ведёт кастомное событие
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Логины с которыми нужно связать событие. Для изменения можно не заполнять
        /// Изменение не пересвязывает событие с логинами
        /// </summary>
        public IReadOnlyList<string> Logins { get; set; } = new List<string>();

        /// <summary>
        /// Признак "не отображать событие в календаре"
        /// </summary>
        public bool IsHidden { get; set; }
    }
}