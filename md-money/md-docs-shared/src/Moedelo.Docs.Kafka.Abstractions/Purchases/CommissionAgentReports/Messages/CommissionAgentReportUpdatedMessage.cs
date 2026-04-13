using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.CommissionAgentReports.Messages
{
    public class CommissionAgentReportUpdatedMessage : ICommissionAgentReportMessage
    {
        /// <summary>
        /// Базовый идентификатор документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Начало периода
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Конец периода
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Идентификатор комиссионера
        /// </summary>
        public int CommissionAgentId { get; set; }

        /// <summary>
        /// Идентификатор склада
        /// </summary>
        public long StockId { get; set; }

        /// <summary>
        /// Наличие НДС у позиций отчёта
        /// </summary>
        public bool UseNds { get; set; }

        /// <summary>
        /// Сумма комиссии
        /// </summary>
        public decimal CommissionSum { get; set; }

        /// <summary>
        /// Тип НДС комиссии
        /// </summary>
        public NdsType? CommissionNdsType { get; set; }

        /// <summary>
        /// Сумма НДС комиссии
        /// </summary>
        public decimal? CommissionNdsSum { get; set; }

        /// <summary>
        /// Идентификатор договора
        /// </summary>
        public long ContractId { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<CommissionAgentReportItemMessage> Items { get; set; }

        /// <summary>
        /// Учитывается в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Штраф
        /// </summary>
        public decimal? Penalty { get; set; }

        /// <summary>
        /// Тип НДС штрафа
        /// </summary>
        public NdsType? PenaltyNdsType { get; set; }

        /// <summary>
        /// Сумма НДС штрафа
        /// </summary>
        public decimal? PenaltyNdsSum { get; set; }

        /// <summary>
        /// Дополнительный доход
        /// </summary>
        public decimal? AdditionalIncome { get; set; }

        /// <summary>
        /// Тип НДС дополнительного дохода
        /// </summary>
        public NdsType? AdditionalIncomeNdsType { get; set; }

        /// <summary>
        /// Сумма НДС дополнительного дохода
        /// </summary>
        public decimal? AdditionalIncomeNdsSum { get; set; }

        /// <summary>
        /// Сабконто отчёта
        /// </summary>
        public long SubcontoId { get; set; }

        /// <summary>
        /// Передана только ЧАСТЬ ДАННЫХ! Модель обогатить через из API в случае необходимости.
        /// </summary>
        public bool IsTruncated { get; set; }

        /// <summary>
        /// Связанные УПД для учёта НДС
        /// </summary>
        public IReadOnlyCollection<NdsLinkedDocumentMessage> LinkedUpds { get; set; } = Array.Empty<NdsLinkedDocumentMessage>();

        /// <summary>
        /// Связанные счета-фактуры для учёта НДС
        /// </summary>
        public IReadOnlyCollection<NdsLinkedDocumentMessage> LinkedInvoices { get; set; } = Array.Empty<NdsLinkedDocumentMessage>();
    }
}