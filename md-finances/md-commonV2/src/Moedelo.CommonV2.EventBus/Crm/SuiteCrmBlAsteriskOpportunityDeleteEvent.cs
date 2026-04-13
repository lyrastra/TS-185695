using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmBlAsteriskOpportunityDeleteEvent
    {
        public string SuiteOpportunityId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}