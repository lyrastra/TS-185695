using System;

namespace Moedelo.HomeV2.Dto.PromoCode
{
    public class CostWithPromoCodeDto
    {
        public int Cost { get; set; }

        public bool IsValid { get; set; }

        public int MonthesAsBonus { get; set; } 

        public DateTime? ExpirationDateAsBonus { get; set; }
    }
}