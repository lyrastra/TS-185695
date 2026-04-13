using System;

namespace Moedelo.Docs.Dto.Docs.PurchasesCommissionAgentReports
{
    public class ReasonDocumentsAutocompleteResponseDto
    {
        /// <summary>
        /// Идентификатор отчёта посредника
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Номер отчёта посредника
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата отчёта посредника
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Доступная сумма для связи
        /// </summary>
        public decimal AvailableSum { get; set; }

        /// <summary>
        /// Сумма комиссии
        /// </summary>
        public decimal CommissionSum { get; set; }

        /// <summary>
        /// Сумма штрафов
        /// </summary>
        public decimal? Penalty { get; set; }
    }
}