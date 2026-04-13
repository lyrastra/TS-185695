using System.Collections.Generic;
using Moedelo.Billing.Shared.Enums;
using Moedelo.Billing.Shared.Enums.Receipts;

namespace Moedelo.Billing.Abstractions.Receipts.Dto;

/// <summary>
/// Запрос на отправку чека об оплате
/// </summary>
public class ReceiptSendRequestDto
{ 
    /// <summary> Email получателя </summary>
    public string NotificationEmail { get; set; }
        
    /// <summary> Причина отправки чека </summary>
    public ReceiptSendReason Reason { get; set; }
        
    /// <summary> Идентификатор фирмы которой отправляется чек </summary>
    public int? FirmId { get; set; }
        
    /// <summary> Идентификатор платежа </summary>
    public int? PaymentHistoryId { get; set; }
        
    /// <summary> Идентификаторы операций выписок </summary>
    public IReadOnlyCollection<int> PaymentImportDetailIds { get; set; }
        
    /// <summary> Идентификаторы успешных платежей в ЮКассе </summary>
    public IReadOnlyCollection<string> YooKassaPaymentGuids { get; set; }
        
    /// <summary> Тип партнера </summary>
    public RegionalPartnerType? RegionalPartnerType { get; set; }
}