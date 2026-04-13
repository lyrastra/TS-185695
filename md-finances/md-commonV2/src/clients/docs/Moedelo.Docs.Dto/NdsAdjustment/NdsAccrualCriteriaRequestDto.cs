using System;

namespace Moedelo.Docs.Dto.NdsAdjustment
{
    public class NdsAccrualCriteriaRequestDto
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
        /// Квартал
        /// </summary>
        public int? Quarter { get; set; }

        /// <summary>
        /// Начало периода
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Конец периода
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Год
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Поле для сортировки
        /// </summary>
        public NdsAccrualSortBy SortBy { get; set; } = NdsAccrualSortBy.Payment;

        /// <summary>
        /// Способ упорядочивания
        /// </summary>
        public OrderBy OrderBy { get; set; } = OrderBy.Asc;
        
        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int? KontragentId { get; set; }
        
        /// <summary>
        /// Идентификатор договора
        /// </summary>
        public int? ContractId { get; set; }

        /// <summary>
        /// Не учитывать пропущенные позиции пользователем
        /// </summary>
        public bool WithoutSkipped { get; set; }
    }
}