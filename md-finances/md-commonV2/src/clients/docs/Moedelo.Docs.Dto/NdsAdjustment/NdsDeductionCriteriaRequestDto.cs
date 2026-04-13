using System;
using System.Collections.Generic;

namespace Moedelo.Docs.Dto.NdsAdjustment
{
    public class NdsDeductionCriteriaRequestDto
    {
        /// <summary>
        /// Количество записей на странице
        /// </summary>
        public int Limit { get; set; } = 20;

        /// <summary>
        /// Смещение начальной позиции чтения в страницах
        /// </summary>
        public int Offset { get; set; } = 0;
        
        /// <summary>
        /// Начало периода
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Конец периода
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Поле для сортировки
        /// </summary>
        public NdsDeductionSortBy SortBy { get; set; } = NdsDeductionSortBy.DocumentDate;

        /// <summary>
        /// Способ упорядочивания
        /// </summary>
        public OrderBy OrderBy { get; set; } = OrderBy.Asc;
        
        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int? KontragentId { get; set; }
        
        /// <summary>
        /// Принято к вычету (частично, не принято)
        /// </summary>
        public List<NdsDeductionState> NdsDeductionState { get; set; }
    }
}