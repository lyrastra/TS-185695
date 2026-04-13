using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Bankruptcy
{
    public class DecisionMessageDto : BaseMessageDto
    {
        public string CourtName { get; set; }

        public DateTime? DecisionDate { get; set; }
    }
}