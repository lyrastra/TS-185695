using System;

namespace Moedelo.AgentsV2.Client.Dto.Lead
{
    public class PartnerLeadRequestDto
    {
        public int PartnerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}