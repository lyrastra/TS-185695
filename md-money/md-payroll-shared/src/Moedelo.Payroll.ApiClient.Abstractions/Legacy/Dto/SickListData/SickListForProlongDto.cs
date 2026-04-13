using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SickListData
{
    public class SickListForProlongDto
    {
        /// <summary>
        /// Идентификатор больничного
        /// </summary>
        public long SpecialScheduleId { get; set; }

        /// <summary>
        /// Дата начала следующего больничного
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Нарушение режима
        /// </summary>
        public bool IsBreachRegim { get; set; }

        /// <summary>
        /// Название больничного
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор типа больничного 
        /// </summary>
        public int TypeId { get; set; }

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