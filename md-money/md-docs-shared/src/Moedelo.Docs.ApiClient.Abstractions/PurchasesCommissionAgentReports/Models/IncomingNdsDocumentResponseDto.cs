using System;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models;

public class IncomingNdsDocumentResponseDto
{
    /// <summary>
    /// Идентификатор документа
    /// </summary>
    public long DocumentBaseId { get; set; }

    /// <summary>
    /// Тип документа. Счёт-фактура или УПД
    /// </summary>
    public int DocumentType { get; set; }

    /// <summary>
    /// Номер документа
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// Дата документа
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Сумма документа (сф или УПД)
    /// </summary>
    public decimal Sum { get; set; }

    /// <summary>
    /// Сумма связи
    /// </summary>
    public decimal NdsSum { get; set; }
}