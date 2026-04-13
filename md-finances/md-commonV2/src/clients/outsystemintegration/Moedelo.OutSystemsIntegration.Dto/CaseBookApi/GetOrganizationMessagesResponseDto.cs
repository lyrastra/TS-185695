using System;
using System.Collections.Generic;
using Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Bankruptcy;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi
{
    public class GetOrganizationMessagesResponseDto
    {
        public List<AuctionMessageDto> Auctions { get; set; }

        public List<DecisionMessageDto> Decisions { get; set; }

        public List<ExecutoryMessageDto> ExecutoryMessages { get; set; }

        public List<MeetingResultMessageDto> MeetingResults { get; set; }

        public List<MeetingMessageDto> Meetings { get; set; }

        public List<BankruptMessageDto> BankruptMessages { get; set; }

        public bool IsError { get; set; }

        public string CaseBookVersion { get; set; }

        public string DataVersion { get; set; }
    }
}
