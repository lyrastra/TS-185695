using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Formula;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SickListData
{
    public class SickListIncomeAndCalculationDto
    {
        /// <summary>
        /// Количество дней
        /// </summary>
        public int DaysCount { get; set; }
        
        /// <summary>
        /// Количество оплачиваемых дней
        /// </summary>
        public int PaidDaysCount { get; set; }

        /// <summary>
        /// Первый год для учёта среднего заработка
        /// </summary>
        public int YearFirst { get; set; }

        /// <summary>
        /// Сумма дохода за первый год
        /// </summary>
        public decimal? YearFirstSum { get; set; }

        /// <summary>
        /// Предупреждение за первый год
        /// </summary>
        public string YearFirstMessage { get; set; }
        
        /// <summary>
        /// Второй год для учёта среднего заработка
        /// </summary>
        public int YearSecond { get; set; }

        /// <summary>
        /// Сумма дохода за второй год
        /// </summary>
        public decimal? YearSecondSum { get; set; }

        /// <summary>
        /// Предупреждение за второй год
        /// </summary>
        public string YearSecondMessage { get; set; }
        
        /// <summary>
        /// Сумма, выплачиваемая ФСС
        /// </summary>
        public decimal PaidByFss { get; set; }

        /// <summary>
        /// Сумма, выплачиваемая работодателем
        /// </summary>
        public decimal PaidByEmployer { get; set; }

        /// <summary>
        /// Участник пилотного проекта
        /// </summary>
        public bool IsFssPilotProject { get; set; }
        
        /// <summary>
        /// Формула
        /// </summary>
        public List<FormulaElementDto> Formula { get; set; }
    }
}