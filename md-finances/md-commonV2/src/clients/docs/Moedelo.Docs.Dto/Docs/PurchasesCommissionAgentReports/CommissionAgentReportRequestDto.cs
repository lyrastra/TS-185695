using System;

namespace Moedelo.Docs.Dto.Docs.PurchasesCommissionAgentReports
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
        /// Отправить запрос на базу для чтения
        /// </summary>
        public bool UseReadonlyDb { get; set; }

        /// <summary>
        /// Вернуть отчеты комиссионера, только те, в которых указаны возвраты
        /// </summary>
        public bool OnlyWithRefunds { get; set; }
        
        public static CommissionAgentReportRequestDto Unlimited => new CommissionAgentReportRequestDto
        {
            Offset = 0,
            Limit = int.MaxValue,
            OnlyWithRefunds = false,
            UseReadonlyDb = false,
            StartDate = null,
            EndDate = null
        };
    }
}