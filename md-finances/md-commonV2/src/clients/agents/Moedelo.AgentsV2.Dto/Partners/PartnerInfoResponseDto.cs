using System;
using Moedelo.Common.Enums.Enums.Partners;

namespace Moedelo.AgentsV2.Dto.Partners
{
    public class PartnerInfoResponseDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public PartnerType Type { get; set; }

        public decimal PartnerAccountRest { get; set; }

        public int LiquidityRatio { get; set; }

        public long ReferalCounter { get; set; }
    }
}
