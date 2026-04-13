
namespace Moedelo.RptV2.Dto.UsnDeclaration
{
    public class UsnTaxProfitAndExpenseDataDto
    {
        /// <summary>
        /// Год
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Доходы
        /// </summary>
        public decimal Profit { get; set; }
        /// <summary>
        /// Расходы
        /// </summary>
        public decimal Expense { get; set; }
        /// <summary>
        /// Ставка
        /// </summary>
        public decimal TaxRate { get; set; }
        /// <summary>
        /// Авансовые платежи
        /// </summary>
        public decimal Prepayments { get; set; }
        /// <summary>
        /// Превышение мин. налога
        /// </summary>
        public decimal MinimalTaxDifferenceSum { get; set; }
        /// <summary>
        /// Убытки прошлых лет
        /// </summary>
        public decimal ReduceIncomeByLossesSum { get; set; }
    }
}
