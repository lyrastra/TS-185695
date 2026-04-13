using System.Collections.Generic;

namespace Moedelo.AgentsV2.Client.Dto.Lead
{
    public class PartnerLeadsResponseDto
    {
        public List<PartnerLeadResponseDto> List { get; set; }
        public int TotalCount { get; set; }
    }
}