using System;

namespace Moedelo.Billing.Abstractions.Legacy.Dto;

public class PaymentShiftRequestDto
{
    public int PaymentId { get; set; }
    public DateTime NewStartDate { get; set; }
    public string ChangesAuthorAppName { get; set; }
}