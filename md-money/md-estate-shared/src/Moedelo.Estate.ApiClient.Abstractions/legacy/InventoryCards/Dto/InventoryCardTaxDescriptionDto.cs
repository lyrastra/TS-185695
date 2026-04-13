using System;

namespace Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards.Dto
{
    public class InventoryCardTaxDescriptionDto
    {
        public DateTime? DateOfStateRegistration { get; set; }
        public DateTime? CommissioningDate { get; set; }
        public decimal Cost { get; set; }
        public decimal? PaidCost { get; set; }
    }
}
