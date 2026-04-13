using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmBlConvertInfoUpdateEvent
    {
        public int? FirmId { get; set; }

        public string SuiteLeadId { get; set; }

        public string SuiteAccountId { get; set; }

        public string SuiteOpportunityId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}