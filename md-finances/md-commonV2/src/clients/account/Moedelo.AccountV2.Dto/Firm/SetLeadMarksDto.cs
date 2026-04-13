using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Leads;

namespace Moedelo.AccountV2.Dto.Firm
{
    public class SetLeadMarksDto
    {
        public List<int> FirmIds { get; set; }

        public LeadMarkType LeadMark { get; set; }

        public int NewId { get; set; }
    }
}
