using System.Collections.Generic;
using Moedelo.BillingV2.Dto.Billing.PaymentPositions;

namespace Moedelo.BillingV2.Dto.Billing;

public class PaymentHistoryAndPositionsDto
{
    public PaymentHistoryDto PaymentHistoryDto { get; set; }
    
    public IReadOnlyCollection<PaymentPositionDto> PaymentPositionDto { get; set; }
}