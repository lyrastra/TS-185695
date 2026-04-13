using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.BackofficeV2.Dto.Reports
{
    public class ReportLeadChannelsParamDto
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public bool New { get; set; }

        public Guid QueryGuid { get; set; }

        public string Email { get; set; }
    }
}
