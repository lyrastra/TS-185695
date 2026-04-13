namespace Moedelo.KudirOsno.Client.IncomeExpense.Dto
{
    public class IncomeExpenseRequestDto
    {
        /// <summary>
        /// Год
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Список кварталов для которых необходимо получить доходы/расходы. Для года - квартал 0
        /// </summary>
        public int[] Quarters { get; set; }
    }
}
