namespace Moedelo.KudirOsno.Client.IncomeExpense.Dto
{
    public class IncomeExpenseResponseDto
    {
        /// <summary>
        /// Квартал
        /// </summary>
        public int Quarter { get; set; }

        /// <summary>
        /// Всего доходов
        /// </summary>
        public decimal TotalIncome { get; set; }

        /// <summary>
        /// Доходы от реализации товаров, работ, услуг
        /// </summary>
        public decimal Income { get; set; }

        /// <summary>
        /// Прочие доходы
        /// </summary>
        public decimal OtherIncome { get; set; }

        /// <summary>
        /// Всего расходов
        /// </summary>
        public decimal TotalExpense { get; set; }

        /// <summary>
        /// Материальные расходы
        /// </summary>
        public decimal SelfCostExpense { get; set; }

        /// <summary>
        /// Амортизационные расходы
        /// </summary>
        public decimal AmortizationExpense { get; set; }

        /// <summary>
        /// Оплата труда
        /// </summary>
        public decimal SalaryExpense { get; set; }

        /// <summary>
        /// Прочие расходы
        /// </summary>

        public decimal OtherExpense { get; set; }
    }
}
