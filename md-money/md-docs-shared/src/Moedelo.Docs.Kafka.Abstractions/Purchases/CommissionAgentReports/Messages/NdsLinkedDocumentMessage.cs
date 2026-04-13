using System;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.CommissionAgentReports.Messages;

/// <summary>
/// Связанный документ для учёта НДС в отчёте комиссионера
/// </summary>
public class NdsLinkedDocumentMessage
{
    /// <summary>
    /// Идентификатор документа
    /// </summary>
    public long DocumentBaseId { get; set; }

    /// <summary>
    /// Дата связи
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Сумма связи
    /// </summary>
    public decimal Sum { get; set; }
}