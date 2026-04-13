using System;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment
{
    /// <summary>
    /// Общая модель для чтения бюджетного периода для БП
    /// </summary>
    public class BudgetaryPeriodResponseDto
    {
        /// <summary>
        /// Тип периода
        /// 1 — ГД (год)
        /// 2 — ПЛ (полугодие)
        /// 3 — КВ (квартал)
        /// 4 — МС (месяц)
        /// 8 — без периода
        /// 9 — дата
        /// </summary>
        public BudgetaryPeriodType Type { get; set; }

        /// <summary>
        /// Дата (при Type = 9)
        /// </summary>
        public DateTime? Date { get; set; }

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