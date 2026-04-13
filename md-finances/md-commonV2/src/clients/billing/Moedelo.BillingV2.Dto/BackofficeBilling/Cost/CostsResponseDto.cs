using System.Collections.Generic;

namespace Moedelo.BillingV2.Dto.BackofficeBilling.Cost
{
    public class CostsResponseDto
    {
        public IReadOnlyCollection<CostResponseDto> Costs { get; set; }
        public PromoCodeApplyingSummaryDto PromoCodeApplyingSummary { get; set; }
        public decimal TotalCost { get; set; }
    }
}