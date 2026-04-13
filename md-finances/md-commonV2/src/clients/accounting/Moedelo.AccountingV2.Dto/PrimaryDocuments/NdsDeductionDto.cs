using System;

namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class NdsDeductionDto
    {
        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
    }
}