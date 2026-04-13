using System;

namespace Moedelo.AccountingV2.Dto.AccountingStatement
{
    public class ReportStatementDataDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма проводки сторно, отмены авансовых платежей за 9 месяцев
        /// </summary>
        public decimal StornoSum { get; set; }

        /// <summary>
        /// Применен минимальный налог
        /// </summary>
        public bool IsMinRate { get; set; }
    }
}
