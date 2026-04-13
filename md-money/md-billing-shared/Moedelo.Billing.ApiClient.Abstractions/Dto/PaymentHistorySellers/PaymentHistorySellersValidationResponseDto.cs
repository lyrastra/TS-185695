using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Dto.PaymentHistorySellers;

public class PaymentHistorySellersValidationResponseDto
{
    public bool IsSuccess { get; set; }

    public IReadOnlyCollection<string> Errors { get; set; }
}