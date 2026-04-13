using System;
using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.RequisitesV2.Dto.NdsRatePeriods
{
    /// <summary>
    /// Настройка (для УСН): Применяемая ставка НДС
    /// </summary>
    public class NdsRatePeriodDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Период действия: начало (включительно)
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Период действия: конец (включительно)
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Тип ставки
        /// </summary>
        public NdsRateType Rate { get; set; }
    }
}