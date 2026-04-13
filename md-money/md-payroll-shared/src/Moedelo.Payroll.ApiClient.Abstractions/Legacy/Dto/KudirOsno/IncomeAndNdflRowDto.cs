using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.KudirOsno
{
    public class IncomeAndNdflRowDto
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int WorkerId { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Дата заключения трудового договора/ГПД
        /// </summary>
        public DateTime StartWorkDate { get; set; }

        /// <summary>
        /// Серия и номер паспорта
        /// </summary>
        public string PassportNumber { get; set; }

        /// <summary>
        /// Кем выдан паспорт
        /// </summary>
        public string PassportIssuer { get; set; }
        
        /// <summary>
        /// Дата выдачи паспорта
        /// </summary>
        public DateTime? PassportIssueDate { get; set; }
        
        /// <summary>
        /// Заработная плата (без вычета НДФЛ)
        /// </summary>
        public decimal ChargeSum { get; set; }

        /// <summary>
        /// Сумма удержаний
        /// </summary>
        public decimal DeductionSum { get; set; }

        /// <summary>
        /// НДФЛ
        /// </summary>
        public decimal NdflSum { get; set; }

        /// <summary>
        /// Дата учета в расходах
        /// </summary>
        public DateTime PaymentDate { get; set; }
    }
}