namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public class ChildCareIncomeAndCalculationDto
    {
        /// <summary>
        /// Количество исключаемых дни
        /// </summary>
        public int ExcludedDaysCount { get; set; }

        /// <summary>
        /// Первый год для учёта среднего заработка
        /// </summary>
        public int YearFirst { get; set; }

        /// <summary>
        /// Сумма дохода за первый год
        /// </summary>
        public decimal? YearFirstSum { get; set; }

        /// <summary>
        /// Второй год для учёта среднего заработка
        /// </summary>
        public int YearSecond { get; set; }

        /// <summary>
        /// Сумма дохода за второй год
        /// </summary>
        public decimal? YearSecondSum { get; set; }

        /// <summary>
        /// Выплачиваемая сумма
        /// </summary>
        public decimal PaidSum { get; set; }
    }
}