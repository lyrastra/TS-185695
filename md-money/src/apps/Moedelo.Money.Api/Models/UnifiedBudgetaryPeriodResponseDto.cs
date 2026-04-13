using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Enums;
using Newtonsoft.Json;

namespace Moedelo.Money.Api.Models
{
    /// <summary>
    /// Общая модель для чтения бюджетного периода для ЕНП
    /// </summary>
    public class UnifiedBudgetaryPeriodResponseDto
    {
        /// <summary>
        /// Тип периода
        /// 1 — ГД (год)
        /// 2 — ПЛ (полугодие)
        /// 3 — КВ (квартал)
        /// 4 — МС (месяц)
        /// 8 — без периода
        /// </summary>
        public BudgetaryPeriodType Type { get; set; }

        /// <summary>
        /// Номер месяца
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Номер кваратала
        /// </summary>
        public int Quarter { get; set; }

        /// <summary>
        /// Номер полугодия
        /// </summary>
        public int HalfYear { get; set; }

        /// <summary>
        /// Год платежа
        /// </summary>
        public int Year { get; set; }
    }
}
