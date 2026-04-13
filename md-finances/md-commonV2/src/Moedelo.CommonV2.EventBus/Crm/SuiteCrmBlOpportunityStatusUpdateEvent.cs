using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmBlOpportunityStatusUpdateEvent
    {
        public string OpportunityStatus { get; set; }

        public string OpportunityDeclineCase { get; set; }

        public string SuiteAccountId { get; set; }

        public string SuiteOpportunityId { get; set; }
        
        public DateTime Timestamp { get; set; }
    }
}