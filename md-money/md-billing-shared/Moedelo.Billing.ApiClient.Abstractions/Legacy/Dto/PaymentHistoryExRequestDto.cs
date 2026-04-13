using System;
using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Legacy.Dto;

/// <summary>
/// критерии поиска платежей
/// все указанные условия складываются через логическое И (умножение)
/// если вам необходимо ИЛИ - делайте несколько вызовов
/// </summary>
public class PaymentHistoryExRequestDto
{
    public int[] PaymentHistoryIds { get; set; } = null;
    public string[] BillNumbers { get; set; } = null;

    /// <summary>
    /// Если указан, то только платежи, со значением BillDate больше или равным указанному значению
    /// </summary>
    public DateTime? BillDateAfter { get; set; }

    /// <summary>
    /// Если указан, то только платежи, со значением BillDate меньше или равным указанному значению
    /// </summary>
    public DateTime? BillDateBefore { get; set; }

    public BillingOperationType[] OperationTypes { get; set; } = null;

    public int[] SalesChannels { get; set; } = null;

    public int[] PrimaryPaymentIds { get; set; } = null;
}