using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Models
{
    /// <summary>
    /// Общая модель для сохранения бюджетного периода для ЕНП
    /// </summary>
    public class UnifiedBudgetaryPeriodSaveDto : IValidatableObject
    {
        /// <summary>
        /// Тип периода
        /// 1 — ГД (год)
        /// 2 — ПЛ (полугодие)
        /// 3 — КВ (квартал)
        /// 4 — МС (месяц)
        /// 8 — без периода
        /// </summary>
        [RequiredValue]
        [Values(BudgetaryPeriodType.NoPeriod, BudgetaryPeriodType.Month, BudgetaryPeriodType.Quarter, BudgetaryPeriodType.HalfYear, BudgetaryPeriodType.Year)]
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
        [RequiredValue]
        public int Year { get; set; }


        private static readonly ValuesAttribute MonthValuesAttribute = new(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        private static readonly ValuesAttribute QuarterValuesAttribute = new(1, 2, 3, 4);
        private static readonly ValuesAttribute HalfYearValuesAttribute = new(1, 2);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            switch (Type)
            {
                case BudgetaryPeriodType.HalfYear:
                    Validator.TryValidateValue(Type, new ValidationContext(this, null, null) { MemberName = "HalfYear" }, results, new[] { HalfYearValuesAttribute });
                    break;
                case BudgetaryPeriodType.Quarter:
                    Validator.TryValidateValue(Type, new ValidationContext(this, null, null) { MemberName = "Quarter" }, results, new[] { QuarterValuesAttribute });
                    break;
                case BudgetaryPeriodType.Month:
                    Validator.TryValidateValue(Type, new ValidationContext(this, null, null) { MemberName = "Month" }, results, new[] { MonthValuesAttribute });
                    break;
            }
            return results;
        }
    }
}
