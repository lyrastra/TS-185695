using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Bankruptcy
{
    public class AuctionMessageDto : BaseMessageDto
    {
        public DateTime? RequestEndDate { get; set; }

        public string RequestRules { get; set; }

        public DateTime? RequestStartDate { get; set; }

        public bool TradeDateSpecified { get; set; }

        public DateTime? TradeDate { get; set; }

        public string TradePlace { get; set; }

        public string TradeType { get; set; }
    }
}