using System;

namespace Moedelo.Docs.Dto.Common
{
    public class PeriodRequestDto
    {
        /// <summary>
        /// (опционально) Фильтр по дате документа: дата больше или равна переданному значению 
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// (опционально) Фильтр по дате документа: дата меньше или равна переданному значению 
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}