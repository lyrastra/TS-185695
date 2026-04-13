using System;
using Moedelo.Payroll.Enums.KudirOsno;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.KudirOsno
{
    public class FundRowDto
    {
        /// <summary>
        /// Тип начисления
        /// </summary>
        public FundRowType RowType { get; set; }

        /// <summary>
        /// Сумма выплаты
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Дата выплаты
        /// </summary>
        public DateTime PaymentDate { get; set; }
        
        /// <summary>
        /// Дата начисления
        /// </summary>
        public DateTime AccrualDate { get; set; }
    }
}