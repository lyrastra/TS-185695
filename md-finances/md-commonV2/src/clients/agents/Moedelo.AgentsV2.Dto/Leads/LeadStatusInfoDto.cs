using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Agents;

namespace Moedelo.AgentsV2.Dto.Leads
{
    public class LeadStatusInfoDto
    {
        public int UserId { get; set; }

        public int FirmId { get; set; }

        public int PartnerLeadId { get; set; }

        public int PartnerId { get; set; }

        public string Login { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public LeadStatus Status { get; set; }
    }
}
