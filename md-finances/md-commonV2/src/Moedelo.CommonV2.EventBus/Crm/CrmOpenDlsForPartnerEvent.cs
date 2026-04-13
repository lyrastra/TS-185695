using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class CrmOpenDlsForPartnerEvent
    {
        public DateTime Timestamp { get; set; }

        public string LeadId { get; set; }
    }
}
