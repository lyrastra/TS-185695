using Moedelo.Common.Enums.Enums.Billing;
using System;

namespace Moedelo.HomeV2.Dto.PromoCode
{
    public class PromoCodeCriteriaDto
    {
        public string PromoCodeName { get; set; }

        public PromoCodeType PromoCodeType { get; set; }

        public Tariff Tariff { get; set; }

        public int PromoCodeMonthCount { get; set; }

        public DateTime CheckDate { get; set; }

        public int FirmId { get; set; }
    }
}
