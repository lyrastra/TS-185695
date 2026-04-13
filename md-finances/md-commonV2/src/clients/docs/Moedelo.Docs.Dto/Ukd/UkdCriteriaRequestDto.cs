using System;

namespace Moedelo.Docs.Dto.Ukd
{
    public class UkdCriteriaRequestDto
    {
        /// <summary>
        /// Смещение начальной позиции чтения в страницах
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Размер страницы в позициях
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// (опционально) Фильтр по дате документа: дата больше или равна переданному значению 
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// (опционально) Фильтр по дате документа: дата меньше или равна переданному значению 
        /// </summary>
        public DateTime? EndDate { get; set; }
        
        /// <summary>
        /// (опционально) Фильтр по наличию хотя бы одного товара в номенклатуре документа (не услуги)
        /// </summary>
        public bool? ProductIdNonNull { get; set; }
    }
}