#nullable enable
using System;
using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto.PaymentHistory;

public class PaymentHistoryExRequestDto
{
    public int[]? PaymentHistoryIds { get; set; } = null;
    public string[]? BillNumbers { get; set; } = null;

    /// <summary>
    /// Если указан, то только платежи, со значением BillDate больше или равным указанному значению
    /// </summary>
    public DateTime? BillDateAfter { get; set; }
    
    /// <summary>
    /// Если указан, то только платежи, со значением BillDate меньше или равным указанному значению
    /// </summary>
    public DateTime? BillDateBefore { get; set; }
    
    /// <summary>
    /// Если указан, то только платежи, со значением BillExpirationDate больше или равным указанному значению
    /// </summary>
    public DateTime? BillExpirationDateAfter { get; set; }
    
    /// <summary>
    /// Если указан, то только платежи, со значением BillExpirationDate меньше или равным указанному значению
    /// </summary>
    public DateTime? BillExpirationDateBefore { get; set; }
    
    public BillingOperationType[]? OperationTypes { get; set; } = null;
    public int[]? SalesChannels { get; set; } = null;
    public int[]? PrimaryPaymentIds { get; set; } = null;
}