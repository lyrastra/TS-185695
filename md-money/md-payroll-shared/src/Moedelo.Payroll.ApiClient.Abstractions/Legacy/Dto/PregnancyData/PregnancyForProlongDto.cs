using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PregnancyData
{
    public class PregnancyForProlongDto
    {
        /// <summary>
        /// Идентификатор декретного
        /// </summary>
        public long SpecialScheduleId { get; set; }

        /// <summary>
        /// Дата окончания продлеваемого декретного
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Дата начала продлеваемого декретного
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Название декретного
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип декретного 
        /// </summary>
        public int PregnancyType { get; set; }

        /// <summary>
        /// Количество исключаемых дни
        /// </summary>
        public int ExcludedDaysCount { get; set; }

        /// <summary>
        /// Номер больничного листа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Первый год для учёта среднего заработка
        /// </summary>
        public int YearFirst { get; set; }

        /// <summary>
        /// Сумма дохода за первый год
        /// </summary>
        public decimal YearFirstSum { get; set; }
        
        /// <summary>
        /// Второй год для учёта среднего заработка
        /// </summary>
        public int YearSecond { get; set; }

        /// <summary>
        /// Сумма дохода за второй год
        /// </summary>
        public decimal YearSecondSum { get; set; }
    }
}