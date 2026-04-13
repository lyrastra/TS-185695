using System;
using Moedelo.Common.Enums.Enums.Partner;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class CrmChangePartnerStatusEvent
    {
        public DateTime Timestamp { get; set; }

        public string Login { get; set; }

        public RegionalPartnerCrmStatus PartnerStatus { get; set; }
    }
}