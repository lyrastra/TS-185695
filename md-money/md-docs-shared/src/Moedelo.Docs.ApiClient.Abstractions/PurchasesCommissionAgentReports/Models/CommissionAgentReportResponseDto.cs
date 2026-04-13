using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models;

/// <summary>
/// отчёт посредника (коммисионера)
/// </summary>
public class CommissionAgentReportResponseDto
{
    /// <summary>
    /// Идентификатор отчета
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
    /// Начало периода
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Конец периода
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Идентификатор комиссионера
    /// </summary>
    public int CommissionAgentId { get; set; }

    /// <summary>
    /// Идентификатор контрагента для коммиссионера
    /// </summary>
    public int KontragentId { get; set; }

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
    public List<CommissionAgentReportItemDto> Items { get; set; }

    /// <summary>
    /// Причина, по которой нельзя редактировать документ
    /// </summary>
    public ReadOnlyReasonType? ReadOnlyReason { get; set; }

    /// <summary>
    /// Признак: документ только для чтение
    /// </summary>
    public bool ReadOnly { get; set; }

    /// <summary>
    /// Логин пользователя последнего изменявшего отчет
    /// </summary>
    public string LastModifiedUserLogin { get; set; }

    /// <summary>
    /// Дата создания отчета
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// Дата последнего изменения отчета
    /// </summary>
    public DateTime ModifyDate { get; set; }

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
    /// Документы для учёта НДС. Счета-фактуры и УПД с 1 статусом
    /// </summary>
    public IReadOnlyCollection<IncomingNdsDocumentResponseDto> IncomingNdsDocuments { get; set; }
}