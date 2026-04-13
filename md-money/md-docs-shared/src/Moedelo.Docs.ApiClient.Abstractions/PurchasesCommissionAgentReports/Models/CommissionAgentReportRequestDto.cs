using System;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models
{
    public class CommissionAgentReportRequestDto
    {
        /// <summary>
        /// Пропустить кол-во отчетов комиссионера (по умолчанию 0)
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// Кол-во отчетов комиссионера (по умолчанию 20)
        /// </summary>
        public int Limit { get; set; } = 20;

        // -- Фильтры --

        /// <summary>
        /// (опционально) Фильтр по дате документа: дата больше или равна переданному значению
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// (опционально) Фильтр по дате документа: дата меньше или равна переданному значению
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// (опционально) Фильтр по подстроке: в номере документа или в имени комиссионера
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// (опционально) Фильтр по подстроке: в позициях отчета
        /// </summary>
        public string ItemQuery { get; set; }

        /// <summary>
        /// (опционально) Фильтр по комиссионеру
        /// </summary>
        public int? CommissionAgentId { get; set; } = null;

        // -- Сортировки --

        /// <summary>
        /// Направление сортировки (по умолчанию asc)
        /// asc - по возрастанию
        /// desc - по убыванию
        /// </summary>
        public string OrderBy { get; set; } = "asc";

        public string SortBy { get; set; } = "documentdate";

        /// <summary>
        /// Отправить запрос на базу для чтения
        /// </summary>
        public bool UseReadonlyDb { get; set; }

        /// <summary>
        /// Вернуть отчеты комиссионера, только те, в которых указаны возвраты
        /// </summary>
        public bool OnlyWithRefunds { get; set; }
    }
}
