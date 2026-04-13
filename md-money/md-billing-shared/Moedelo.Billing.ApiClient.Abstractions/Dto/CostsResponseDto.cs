using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Dto;

public class CostsResponseDto
{
    public IReadOnlyCollection<CostResponseDto> Costs { get; set; }
    public PromoCodeApplyingSummaryDto PromoCodeApplyingSummary { get; set; }
    public decimal TotalCost { get; set; }
}