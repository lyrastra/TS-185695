using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.CommonV2.EventBus.Backoffice.Reports
{
    public class SendReportLeadChannels2Event
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public bool New { get; set; }

        public Guid QueryGuid { get; set; }

        public string Email { get; set; }
    }
}
