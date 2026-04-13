using System;

namespace Moedelo.Billing.Abstractions.Legacy.Dto;

public class UpdateIncomingDateRequestDto
{
    public int PaymentHistoryId { get; set; }
    public DateTime? IncomingDate { get; set; }
}