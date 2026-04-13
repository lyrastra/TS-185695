using System;

namespace Moedelo.Billing.Abstractions.Legacy.Dto;

public class SwitchOnPaymentRequestDto
{
    public PaymentHistoryDto PaymentHistory { get; set; }

    [Obsolete("Typo")]
    public PaymentHistoryDto PaymenHistory => PaymentHistory;

    public bool TruncateCurrentPaymentAnyway { get; set; }
    public int UserId { get; set; }
}