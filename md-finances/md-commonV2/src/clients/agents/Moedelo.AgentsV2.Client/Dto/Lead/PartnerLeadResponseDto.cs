using System;
using Moedelo.Common.Enums.Enums.Account;
using Moedelo.Common.Enums.Enums.Agents;

namespace Moedelo.AgentsV2.Client.Dto.Lead
{
    public class PartnerLeadResponseDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FirmId { get; set; }

        public string Login { get; set; }

        public LeadStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsOoo { get; set; }

        public ProductPlatform ProductPlatform { get; set; }
    }
}