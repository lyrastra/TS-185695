using System;

namespace Moedelo.Docs.Dto.Docs.PurchasesCommissionAgentReports
{
    public class ReasonDocumentsAutocompleteRequestDto
    {
        /// <summary>
        /// Часть номера документа
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int? KontragentId { get; set; }

        /// <summary>
        /// Необходимая сумма свободная для связи
        /// </summary>
        public decimal? AvailableSum { get; set; }

        /// <summary>
        /// Дата сф или УПД для попадания в период отчёта посредника
        /// </summary>
        public DateTime? DocumentDate { get; set; }

        /// <summary>
        /// Кол-во результатов
        /// </summary>
        public int Count { get; set; } = 5;
    }
}