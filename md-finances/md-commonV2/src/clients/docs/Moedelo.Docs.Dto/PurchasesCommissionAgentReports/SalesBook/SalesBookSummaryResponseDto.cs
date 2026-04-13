using System;

namespace Moedelo.Docs.Dto.PurchasesCommissionAgentReports.SalesBook
{
    public class SalesBookSummaryResponseDto
    {
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Агрегированные позиции по дате отгрузки и типу НДС
        /// </summary>
        public SalesBookSummaryItemResponseDto[] Items { get; set; }
    }
}
