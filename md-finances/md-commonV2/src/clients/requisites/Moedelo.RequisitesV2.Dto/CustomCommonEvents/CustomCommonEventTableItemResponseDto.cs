using System;
using Moedelo.Common.Enums.Enums.Calendar;

namespace Moedelo.RequisitesV2.Dto.CustomCommonEvents
{
    public class CustomCommonEventTableItemResponseDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

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
        /// Тип ссылки, на которое ведёт событие
        /// </summary>
        public UrlType UrlType { get; set; }

        /// <summary>
        /// Ссылка, на которое ведёт событие
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Пользователь создавший/изменивший событие
        /// </summary>
        public int ModifyUserId { get; set; }

        /// <summary>
        /// Признак "не отображать событие в календаре"
        /// </summary>
        public bool IsHidden { get; set; }
    }
}