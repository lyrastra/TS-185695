using System;
using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto.PaymentHistory;

/// <summary>
/// Инофрмация о счёте
/// </summary>
public class PaymentHistoryExDto
{
    /// <summary>
    /// Идентификатор истории платежей
    /// </summary>
    public int PaymentHistoryId { get; set; }

    /// <summary>
    /// Номер счёта
    /// </summary>
    public string BillNumber { get; set; }

    /// <summary>
    /// Дата выставления счёта
    /// </summary>
    public DateTime BillDate { get; set; }

    /// <summary>
    /// Канал продаж
    /// </summary>
    public int SalesChannel { get; set; }

    /// <summary>
    /// Тип операции в биллинге
    /// </summary>
    public BillingOperationType OperationType { get; set; }

    /// <summary>
    /// Флаг выставления счёта Аутсорса (ГУ)
    /// </summary>
    public bool IsOutsource { get; set; }

    public int? FirstPayId { get; set; }
    public int? PrimaryPaymentId { get; set; }
}
