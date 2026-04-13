using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Docs.Dto.PurchasesCommissionAgentReports.SalesBook
{
    public class SalesBookSummaryItemResponseDto
    {
        /// <summary>
        /// Дата отгрузки
        /// </summary>
        public DateTime ShipmentDate { get; set; }

        /// <summary>
        /// Тип НДС
        /// </summary>
        public NdsType NdsType { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal NdsSum { get; set; }

        /// <summary>
        /// Сумма с НДС
        /// </summary>
        public decimal SumWithNds { get; set; }

        /// <summary>
        /// Коды НДС
        /// </summary>
        public string[] NdsCodes { get; set; }
    }
}
