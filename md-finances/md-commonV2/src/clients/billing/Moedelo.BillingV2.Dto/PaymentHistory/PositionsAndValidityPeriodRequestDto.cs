using System;
using System.Collections.Generic;
using Moedelo.BillingV2.Dto.Billing.PaymentPositions;

namespace Moedelo.BillingV2.Dto.PaymentHistory;

public class PositionsAndValidityPeriodRequestDto
{
    public int PaymentHistoryId { get; set; }
    public IReadOnlyCollection<PaymentPositionDto> PositionDtos { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
}