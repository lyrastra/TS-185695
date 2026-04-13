using System;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.HomeV2.Dto.PromoCode
{
    public class GetCostWithPromoCodeRequestDto
    {
        public int Cost { get; set; }

        public string PromoCodeName { get; set; }

        public PromoCodeType PromoCodeType { get; set; }

        public Tariff Tariff { get; set; }

        public int Period { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int FirmId { get; set; }
    }
}