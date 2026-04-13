using System;

namespace Moedelo.Docs.Dto.Docs.PurchasesCommissionAgentReports
{
    public class CommissionAgentReportAsReasonDocumentDto
    {
        /// <summary>
        /// Идентификатор отчёта посредника
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Идентификатор комиссионера
        /// </summary>
        public int CommissionAgentId { get; set; }

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
        /// Сумма НДС штрафа
        /// </summary>
        public decimal? PenaltyNdsSum { get; set; }
    
        /// <summary>
        /// Сумма НДС комиссии
        /// </summary>
        public decimal? CommissionNdsSum { get; set; }

        /// <summary>
        /// Субконто отчета комиссионера
        /// </summary>
        public long SubcontoId { get; set; }
    }
}